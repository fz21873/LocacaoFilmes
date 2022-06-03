export default class Service {

    constructor(resource, urlServidor) {
        if (!resource)

            throw 'Erro instanciando a classe Service: Parâmetro obrigatório (resource)';
        if (!urlServidor) {
            // throw 'Erro instanciando a classe Service: Todos os parâmetros são obrigatórios (resource,  urlServidor)';
            this._resource = resource;
        } else {
            this._resource = resource(urlServidor,
                null,
                null,
                null);
        }

    }

    //GET
    getAll() {



        return this._resource
            .query()
            .then(res => res.json());
    }

    //GET
    getById(id) {
        return this._resource
            .query({
                id
            })
            .then(res => res.json());
    }

    //DELETE
    delete(id) {
        return this._resource
            .delete({
                id
            })
            .then(res => res.json());
    }

    //POST
    create(objeto) {
        return this._resource
            .save(objeto)
            .then(res => res.json());
    }

    //PUT
    update(objeto) {

        return this._resource
            .update({
                id: objeto.codigo
            }, objeto);
    }
}
