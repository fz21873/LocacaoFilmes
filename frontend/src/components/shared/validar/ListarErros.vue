<template>
    <v-alert class="margem" transition="scale-transition" :value="componenteVisivel" color="error" icon="warning" outlined>
        <h4>Os seguintes erros devem ser corrigidos:</h4>
        <ul>
            <li id="listaErroTela" v-for="(erro,index) in mensagens" :key="`erro-${index}`">
                {{ erro }}
            </li>
        </ul>
    </v-alert>
</template>

<script>

    export default {
        props: {
            visivel: {
                type: Boolean,
                required: true
            },
            erros: {
                required: true
            },

            rolarTopo:{
                type: Boolean,
                required: false,
                default: true
            }
        },

        methods:{
           //rola de maneira suave para o topo da pagina
       // eslint-disable-next-line vue/no-dupe-keys
       rolarTopos(){
      try {
          window.scrollTo({
              'behavior': 'smooth',
              'left': 0,
              'top': document.querySelector('#app').offsetTop
          });
      } catch (error) {
          if (error instanceof TypeError) {
              window.scroll(0, 0);
          } else {
              throw error;
          }
      }
  }
        },
        computed: {


            mensagens() {
                let errosLista = [];
                      try {
                    for (var propriedade in this.erros) {
                          if (this.erros.hasOwnProperty(propriedade)) {
                            this.erros[propriedade].forEach(erro => {
                                errosLista.push(erro);
                            });
                        }
                    }
                } catch (e) {
                    console.error(e);
                    errosLista = [];
                }
                return errosLista;
            },
            componenteVisivel() {
                if (this.visivel && this.mensagens.length > 0) {
                    if(this.rolarTopo){
                        rolarTopos();
                    }
                    return true;
                }
                return false;
            }
        }
    };
</script>

<style>
    .margem {
        margin: 15px;
    }
</style>
