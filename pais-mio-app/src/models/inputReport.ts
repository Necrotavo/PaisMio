import { ReportedInput } from './reportedInput';
import { InfoPaisMio } from './infoPaisMio';
export class InputReport {
    listaInsumos: ReportedInput[];
    infoPaisMio: InfoPaisMio;
    fechaInicio: string;
    fechaFinal: string;

    constructor(listaInsumos: ReportedInput[], infoPaisMio: InfoPaisMio, fechaInicio: string, fechaFinal: string){
        this.listaInsumos = listaInsumos;
        this.infoPaisMio = infoPaisMio;
        this.fechaInicio = fechaInicio;
        this.fechaFinal = fechaFinal;
    }
}

