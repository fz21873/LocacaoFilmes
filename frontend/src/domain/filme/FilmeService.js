import Service from '../Service';

export default class FilmeService extends Service {
    constructor(resource) {
        super(resource, 'api/Film{/id}');

    }

}
