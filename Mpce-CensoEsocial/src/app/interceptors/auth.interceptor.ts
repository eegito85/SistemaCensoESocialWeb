//import { UsrStoraged } from '../model/usr-storaged.model';
import { StorageService } from '../shared/storage.service';
import { Injectable } from "@angular/core";
import { HttpInterceptor, HttpHandler, HttpRequest, HttpEvent, HTTP_INTERCEPTORS } from "@angular/common/http";
import { Observable } from "rxjs";


@Injectable()
export class AuthInterceptor implements HttpInterceptor {

    constructor(private storageService: StorageService){}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>>{

        let usr: any = this.storageService.getUsuarioLogado();
        if(usr){
            const authReq = req.clone({headers: req.headers.set('Authorization','Bearer ' + usr)})
            return next.handle(authReq);
        }else {
            return next.handle(req)
        }
              
    }


}

export const AuthInterceptorProvider = {
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true
}
