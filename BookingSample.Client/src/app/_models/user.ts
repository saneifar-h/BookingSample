export class User {
    Username: string;
    Token?: string;
}

export class RoomAvailableInfoDto {
    RoomId: number;
    AvailablePlaces: number;
}

export class AvailableInfoDto {
    AvailableItems:RoomAvailableInfoDto[] ;
     TotalItems:number
     TotalPages:number
     CurrentPage:number
}
