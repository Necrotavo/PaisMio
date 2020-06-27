import { InputCompared } from './inputCompared';
import { InfoPaisMio } from './infoPaisMio';
export class InputComparativeReport {
    listaInsumos: InputCompared[];
    infoPaisMio: InfoPaisMio;
    inicioMes1: string;
    finalMes1: string;
    inicioMes2: string;
    finalMes2: string;

    constructor(listaInsumos: InputCompared[], infoPaisMio: InfoPaisMio, inicioMes1: string,
                finalMes1: string, inicioMes2: string, finalMes2: string){
        this.listaInsumos = listaInsumos;
        this.infoPaisMio = infoPaisMio;
        this.inicioMes1 = inicioMes1;
        this.finalMes1 = finalMes1;
        this.inicioMes2 = inicioMes2;
        this.finalMes2 = finalMes2;
    }
}
