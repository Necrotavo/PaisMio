import { InputRequest } from './inputRequest';
import { User } from './user';

export class InputRequestDesicion {
    solicitud: InputRequest;
    admin: User;
    estado: string;

    constructor(solicitud: InputRequest, admin: User, estado: string) {
        this.solicitud = solicitud;
        this.admin = admin;
        this.estado = estado;
    }
}
