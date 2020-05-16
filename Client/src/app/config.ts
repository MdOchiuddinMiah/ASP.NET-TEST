const config = {

    api: {
        tokenLabel: 'Authorization',
        tokenValue: token => 'Bearer ' + token,
        contentType: 'application/json'
    },
    debounce: {
        interval: 300, // milliseconds;
        searchInterval: 1000,
    },
    storage: {
        app: 'app',
        theme: 'theme'
    },
    routes: {
        home: '',
        dashboard: 'dashboard'
    },
    apiendpoint: {
        posts: 'app-survey/posts',
        feedback: 'app-survey/feedback'
    },
}

export default config;
