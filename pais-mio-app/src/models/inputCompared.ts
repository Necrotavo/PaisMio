import { ReportedInput } from './reportedInput';
export class InputCompared {
    insumoPrimerMes: ReportedInput;
    insumoSegundoMes: ReportedInput;
    diferenciaConsumir: number;
    diferenciaDescarte: number;
    diferenciaTotal: number;

    constructor(insumoPrimerMes: ReportedInput, insumoSegundoMes: ReportedInput,
                diferenciaConsumir: number, diferenciaDescarte: number, diferenciaTotal: number){
        this.insumoPrimerMes = insumoPrimerMes;
        this.insumoSegundoMes = insumoSegundoMes;
        this.diferenciaConsumir = diferenciaConsumir;
        this.diferenciaDescarte = diferenciaDescarte;
        this.diferenciaTotal = diferenciaTotal;
    }
}