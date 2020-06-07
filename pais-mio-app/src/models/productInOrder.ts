import {Product} from './product';
export class ProductInOrder {
    producto: Product;
    cantidad: number;

    constructor(producto: Product, cantidad: number){

        this.producto = producto;
        this.cantidad = cantidad;
    }
}