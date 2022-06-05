import Vue from 'vue';
import App from './App';
import store from './store';
import Vuetify from 'vuetify';
import VueRouter from 'vue-router';
import router from './router';
import 'vuetify/dist/vuetify.css';
import 'material-design-icons-iconfont/dist/material-design-icons.css';
import apiConfig from './api.config';
import VueJsonp from 'vue-jsonp';
import VueResource from 'vue-resource';
import '@/assets/global.css';

import '@mdi/font/css/materialdesignicons.css';
import moment from 'moment';
import VueMask from 'v-mask';

window.$ = require('vue');
window.JQuery = require('jquery');
Vue.config.productionTip = false;

Vue.use(VueMask);
const opts = {
    theme: {
        themes: {
            light: {
                primary: '#000080',
                tecmicro: '#000080',
            }
        }
    },
    icons: {
        iconfont: 'md',
    },
};
Vue.use(VueResource);
const vuetify = new Vuetify(opts);
Vue.http.options.root = (apiConfig.https ? 'https' : 'http') + '://' + apiConfig.endereco + ':' + apiConfig.porta;

// Plugin para ler response sem COR
Vue.use(VueJsonp);
/* eslint-disable no-new */

Vue.use(Vuetify);
new Vue({
    vuetify: vuetify,
    el: '#app',
    router,
    store,
    render: h => h(App)
}).$mount('#app');
