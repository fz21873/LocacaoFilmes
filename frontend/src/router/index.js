import Vue from 'vue';
import Router from 'vue-router';
import DiretorHome from '../components/cadastros/diretor/DiretorHome.vue';
import DiretorListar from '../components/cadastros/diretor/DiretorListar.vue';
import DiretorCadastrar from '../components/cadastros/diretor/DiretorCadastrar.vue';

import FilmeHome from '../components/cadastros/filme/FilmeHome.vue';
import FilmeListar from '../components/cadastros/filme/FilmeListar.vue';
import FilmeCadastrar from '../components/cadastros/filme/FilmeCadastrar.vue';

Vue.use(Router);

export default new Router({
    routes: [{
            path: '/',
            name: 'Home',
            component: DiretorListar

        },

        {

            path: '/diretor',
            redirect: '/diretor/listar',
            component: DiretorHome,

            children: [{
                    path: 'cadastrar',
                    component: DiretorCadastrar,
                    meta: {

                        titulo: 'Cadastrar diretor',

                    }
                },
                {
                    path: 'editar/:id',
                    component: DiretorCadastrar,
                    meta: {

                        titulo: 'Editar diretor',

                    }
                },
                {
                    path: 'listar',
                    component: DiretorListar,
                    meta: {

                        titulo: 'Listar diretores',

                    }
                }
            ]
        },

        {
            path: '/filme',
            redirect: '/filme/listar',
            component: FilmeHome,

            children: [{
                    path: 'cadastrar',
                    component: FilmeCadastrar,
                    meta: {

                        titulo: 'Cadastrar filme',

                    }
                },
                {
                    path: 'editar/:id',
                    component: FilmeCadastrar,
                    meta: {

                        titulo: 'Editar filme',

                    }
                },
                {
                    path: 'listar',
                    component: FilmeListar,
                    meta: {

                        titulo: 'Listar filme',

                    }
                }
            ]
        },

    ]
});
