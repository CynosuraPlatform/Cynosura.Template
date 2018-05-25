import { Injectable } from "@angular/core";
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse } from "@angular/common/http";
import { Observable } from "rxjs/Observable";
import { tap } from "rxjs/operators/tap";

import { AuthService  } from "./services/auth.service";

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

    constructor(private authService: AuthService) {}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const tokens = this.authService.tokens();
        if (tokens != null) {
            request = request.clone({
                setHeaders: {
                    Authorization: `Bearer ${tokens.access_token}`
                }
            });
        }
        return next.handle(request)
            .pipe(tap(
                    (event: HttpEvent<any>) => {

                    }, (err: any) => {
                        if (err instanceof HttpErrorResponse) {
                            throw err.error;
                        }
                    })
            );
    }
}
