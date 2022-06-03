import axios from 'axios';

/*const api = axios.create({
    baseURL: 'http://localhost:43203/', //url base da sua api
    timeout: 15000 //tempo máximo de espera de resposta
});*/
const api = {
    https: false,
    porta: 43203,
    endereco: 'localhost',
    client: axios.create({
        baseURL: 'https://localhost:43203',
        timeout: 15000,
        headers: {
            'Content-Type': 'application/json',
            'Access-Control-Allow-Origin': '*',
        }
    }),
};

export default api;
