import { ProductInOrder } from './productInOrder';
export class Order {
    codigo: number;
    cedulaCliente: String;
    correoAdminIngreso : String;
    correoAdminDespacho : String;
    estado: String;
    fecheIngreso : String;
    fechaDespacho : String;
    listaProductos : ProductInOrder[];

    constructor(codigo : number, cedulaCliente: String, correoAdminIngreso: String, listaProductos: ProductInOrder[]){

        this.codigo = codigo;
        this.cedulaCliente = cedulaCliente;
        this.correoAdminIngreso = correoAdminIngreso;
        this.listaProductos = listaProductos;

    }

}