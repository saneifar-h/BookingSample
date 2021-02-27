import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '@environments/environment';
import { User } from '@app/_models';

@Injectable({ providedIn: 'root' })
export class BookingService {
    constructor(private http: HttpClient) { }

    getAll(startDate:Date,endDate:Date) {
        return this.http.get<User[]>(`${environment.apiUrl}/users`);
    }
}