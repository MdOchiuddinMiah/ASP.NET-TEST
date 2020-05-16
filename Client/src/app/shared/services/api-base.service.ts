import { Injectable, Injector } from '@angular/core';
import { Http, Response, Headers, URLSearchParams } from '@angular/http';
import { ConnectionBackend, RequestOptions, RequestOptionsArgs, HttpModule } from '@angular/http';
import { Observable } from 'rxjs';
import { map, share, catchError, debounceTime } from 'rxjs/operators';
import config from '../../config';

@Injectable()
export class ApiBaseService extends Http {

  requests: boolean = false;
  _requestsCount: number = 0;

  constructor(
    protected _backend: ConnectionBackend,
    protected _defaultOptions: RequestOptions
  ) {
    super(_backend, _defaultOptions);
  }

  private _headers(options?: RequestOptionsArgs) {
    options = options || {};
    options['headers'] = options.headers || new Headers();
    let app = JSON.parse(localStorage.getItem(config.storage.app));
    // set content type
    options.headers.append('Content-Type', config.api.contentType);
    if (app && app.token) {
      options.headers.append('token', app.token);
    }
    return options;
  }

  private _snakecase(obj: Object) {
    let _regex = /([a-z])([A-Z])/g;

    return obj ? Object.keys(obj).reduce((output, item) => {
      output[item.replace(_regex, '$1_$2').toLowerCase()] = obj[item];
      return output;
    }, {}) : obj;
  }

  private _camelcase(obj: Object) {
    let _regex = /(_)(\w)/g;

    return obj ? Object.keys(obj).reduce((output, item) => {
      output[item.replace(_regex, '$2').toUpperCase()] = obj[item];
      return output;
    }, {}) : obj;
  }

  private _serialize(value: any): string {
    return JSON.stringify(value) || '{}';
  }

  private _deserialize(res: Observable<Response>): any {
    return res
      .pipe(map(res => {
        let data = res['_body'] && res.json() || {};
        return (data);
      }), catchError(res => {
        if (res.status >= 500 && res.status < 600) {
          let data: any;
          try {
            data = res.json() || {};
          } catch (e) {
            data = this._error(res.text(), res.status);
          }
          return Observable.throw(res.text());
        }
        if (res.status >= 400 && res.status < 500) {
          let data = res.json() || {};
          return Observable.throw(data);
        }
        return Observable.throw(res.text());
      }));
  }

  private _encode(params?: any): URLSearchParams {
    if (params instanceof URLSearchParams) return params;
    let searchParams = new URLSearchParams();
    Object.keys(params).forEach(key => params[key] !== null && searchParams.set(key, params[key]));
    return searchParams;
  }

  private _error(message: string, status: number = 400): any {
    return {
      error: {
        code: status,
        message: message || 'Server error (${status})',
      }
    };
  }

  get(url: string, params?: any, options?: RequestOptionsArgs): Observable<any> {
    options = this._headers(options);
    params && (options.search = this._encode(this._snakecase(params)));
    let res = super.get(this.getServiceURL(url), options).pipe(share());
    this.setStatus(res);
    return this._deserialize(res);
  }

  post(url: string, body?: any, params?: any, options?: RequestOptionsArgs): Observable<any> {

    options = this._headers(options);
    body = this._serialize(body);

    params && (options.search = this._encode(params));
    let res = super.post(this.getServiceURL(url), body, options).pipe(share());
    this.setStatus(res);
    let result = this._deserialize(res);
    return result;
  }

  put(url: string, body: any, params?: any, options?: RequestOptionsArgs): Observable<any> {
    options = this._headers(options);
    body = this._serialize(body);
    params && (options.search = this._encode(params));

    let res = super.put(this.getServiceURL(url), body, options).pipe(share());

    this.setStatus(res);

    return this._deserialize(res);
  }

  delete(url: string, params?: any, options?: RequestOptionsArgs): Observable<any> {
    options = this._headers(options);
    params && (options.search = this._encode(params));

    let res = super.delete(this.getServiceURL(url), options).pipe(share());

    this.setStatus(res);

    return this._deserialize(res);
  }

  setStatus(observable: Observable<any>) {
    this._requestsCount++;
    this.requests = true;

    observable.pipe(debounceTime(config.debounce.interval)).subscribe(
      () => { },
      () => { },
      () => {
        this._requestsCount--;
        this.requests = !!this._requestsCount;
      },
    );
  }

  getStatus(type: string) {
    return !!this.requests;
  }

  getServiceURL(url: string) {
    return window.location.protocol + '//' + url;
  }

}

