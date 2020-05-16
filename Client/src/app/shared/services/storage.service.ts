import { Injectable } from '@angular/core';

@Injectable()
export class Storage {

  get(key: string): any {
    const value = localStorage.getItem(key);
    try {
      return value == undefined ? undefined : JSON.parse(value);
    } catch (err) {
      return value;
    }
  }

  set(key: string, value: any): void {
    localStorage.setItem(key, JSON.stringify(value));
  }

  getSession(key: string): any {
    const value = sessionStorage.getItem(key);
    try {
      return value == undefined ? undefined : JSON.parse(value);
    } catch (err) {
      return value;
    }
  }

  setSession(key: string, value: any): void {
    sessionStorage.setItem(key, JSON.stringify(value));
  }

  remove(key: string): void {
    localStorage.removeItem(key);
  }

  clear(): void {
    localStorage.clear();
  }
}
