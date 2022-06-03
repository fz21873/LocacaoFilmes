export default {
    data() {
        return {
            enviandoDados: false,
            alerta: false,
            alertaTexto: '',
            alertaCor: '',
            alertaCorTexto: '',
            alertaIcone: '',
            carregandoValores: true,
            carregandoLista: false,
            paramId: null,
            errosServidor: null,
            modificado: false, //em relacao ao cadastro
            acabouDeSalvar: false,
            modalConfirmacao: false,
            botaoAlteracoes: false,
            criadoPor: '',
            alteradoPor: '',
            criadoEm: '',
            alteradoEm: '',
            buscandoDadosTabela: 'Buscando dados...',
        };
    },
    mounted() {

        if (!this.paramId) {
            this.carregandoValores = false;
        }
    }
};
