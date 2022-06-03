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


    static validaData(data, maiorQueDataAtual = false) {

        if (data.length <= 10) {

            let dataCruaSplitada = data.split('');
            let dia = dataCruaSplitada[0] + dataCruaSplitada[1];
            let mes = dataCruaSplitada[3] + dataCruaSplitada[4];
            let ano = dataCruaSplitada[6] + dataCruaSplitada[7] + dataCruaSplitada[8] + dataCruaSplitada[9];

            if (mes <= 12) {
                let diasMes = this.quantidadeDeDiaMes(ano, mes);
                if (dia <= diasMes) {

                    let dataPronta = ano + '-' + mes + '-' + dia;

                    if (maiorQueDataAtual) {
                        if (dataPronta >= new Date().toISOString().substr(0, 10)) {
                            return dataPronta;
                        } else {

                            return true;
                        }
                    } else {


                        return dataPronta;
                    }
                }
            }
        }
        return false;
    };


    static quantidadeDeDiaMes(ano, mes) {
        if (ano != null && mes != null) {
            return new Date(ano, mes, 0).getDate();
        }
    };
}
