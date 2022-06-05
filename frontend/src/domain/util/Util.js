import Confirmacao from '@/components/shared/validar/Confirmacao.vue';

import Vue from 'vue';
export default class Util {

    static exibirConfimacao(mensagem, options = {}) {

        options.mensagem = mensagem;
        debugger;
        const ComponentClass = Vue.extend(Object.assign(Confirmacao));
        const container = document.querySelector('#confirmar') || document.body;

        return new Promise(resolve => {
            const instance = new ComponentClass(Object.assign({}, {
                propsData: {
                    options: options,
                    visivel: true
                },
                destroyed: () => {
                    container.removeChild(instance.$el);
                    resolve(instance.value);
                },
            }));

            container.appendChild(instance.$mount().$el);
        });



    }


    static validaData(data) {

        if (data.length <= 10) {
            return true;
        }
        return false;
    };

    static debounce(fun, mili) {
        var timer;
        var copy;
        return function () {
            clearTimeout(timer);
            copy = arguments;
            timer = setTimeout(function () {
                fun.apply(null, copy);
            }, mili);
        };
    }

}
