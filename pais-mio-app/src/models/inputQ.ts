import { Input } from './input';

export class InputQ {
    cantidad: number;
    input: Input;

    constructor(cantidad: number, input: Input) {
        this.cantidad = cantidad;
        this.input = input;
    }
}
