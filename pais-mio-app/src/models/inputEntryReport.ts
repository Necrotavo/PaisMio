import { InputEntryReported } from './inputEntryReported';
import { InfoPaisMio } from './infoPaisMio';
export class InputEntryReport {
    listaEntradas: InputEntryReported[];
    infoPaisMio: InfoPaisMio;
    fechaInicio: string;
    fechaFinal: string;

    constructor(listaEntradas: InputEntryReported[], infoPaisMio: InfoPaisMio, fechaInicio: string, fechaFinal: string){
        
        this.listaEntradas = listaEntradas;
        this.infoPaisMio = infoPaisMio;
        this.fechaInicio = fechaInicio;
        this.fechaFinal = fechaFinal;
    }
}
