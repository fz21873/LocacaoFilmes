import Service from '../Service';

export default class DiretorService extends Service {
    constructor(resource) {
        super(resource, 'api/Director{/id}');

    }

}
