import { Component } from '@angular/core';
import { first } from 'rxjs/operators';

import { User,AvailableInfoDto,RoomAvailableInfoDto } from '@app/_models';
import { UserService } from '@app/_services';

@Component({ templateUrl: 'home.component.html' })
export class HomeComponent {
    loading = false;
    rooms: AvailableInfoDto;


    constructor(private userService: UserService) { }

    ngOnInit() {
        this.loading = true;
        this.userService.default().pipe(first()).subscribe(users => {
            this.loading = false;
            this.rooms = users;
        });
    }
}