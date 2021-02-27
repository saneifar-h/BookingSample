import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '@environments/environment';
import { AvailableInfoDto, User } from '@app/_models';

@Injectable({ providedIn: 'root' })
export class UserService {
    constructor(private http: HttpClient) { }

    default() {
        return this.http.get<AvailableInfoDto>(`${environment.apiUrl}/booking/get`);
    }
}