import { AnalysisPC } from './analysisPC';
export class Analysis {
    pedCodigo: number;
    ipmCodigo: number;
    aSensorial: number;
    exGustativo: number;
    exOlfativo: number;
    exVisual: number;
    fechaEmision: string;
    fechaVigencia: string;
    nombreProducto: string;
    notas: string;
    analisisFQs: AnalysisPC[];

    constructor(pedCodigo: number,ipmCodigo: number, aSensorial: number, exGustativo: number, exOlfativo: number,
        exVisual: number, fechaEmision: string, fechaVigencia: string, nombreProducto: string, notas: string, analisisFQs: AnalysisPC[]){
        this.pedCodigo = pedCodigo;
        this.ipmCodigo = ipmCodigo;
        this.aSensorial = aSensorial;
        this.exGustativo = exGustativo;
        this.exOlfativo = exOlfativo;
        this.exVisual = exVisual;
        this.fechaEmision = fechaEmision;
        this.fechaVigencia = fechaVigencia;
        this.nombreProducto = nombreProducto;
        this.notas = notas;
        this.analisisFQs = analisisFQs;
    }
}
