import Director from '../diretor/Diretor';

export default class Filme {
    constructor(
        id = 0,
        titulo = '',
        descricao = '',
        tipo = '',
        dataLancamento = '',
        directorId = 0,
        diretor = new Director

    ) {
        this.id = id;
        this.titulo = titulo;
        this.descricao = descricao;
        this.tipo = tipo;
        this.dataLancamento = dataLancamento;
        this.directorId = directorId;
        this.diretor = diretor;
    }
}
