export class Client {
    cedula: string;
    correo: string;
    direccion: string;
    estado: string;
    nombre: string;
    telefono: string;

    constructor(cedula: string, correo: string, direccion: string, estado: string, nombre: string, telefono: string) {
        this.cedula = cedula;
        this.correo = correo;
        this.direccion = direccion;
        this.estado = estado;
        this.nombre = nombre;
        this.telefono = telefono;
    }
}


