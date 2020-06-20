export class AnalysisPC {
    pedCodigo: number;
    tipoAnalisisFQ: string;
    medicionResultado: string;
    unidadCondicion: string;

    constructor(pedCodigo: number, tipoAnalisisFQ: string, medicionResultado: string, unidadCondicion: string){
        this.pedCodigo = pedCodigo;
        this.tipoAnalisisFQ = tipoAnalisisFQ;
        this.medicionResultado = medicionResultado;
        this.unidadCondicion = unidadCondicion;
    }
}
