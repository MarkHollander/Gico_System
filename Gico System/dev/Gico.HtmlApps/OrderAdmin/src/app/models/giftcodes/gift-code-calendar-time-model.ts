export class GiftCodeCalendarTimeModel {
    date: Date;
    times: number[]
}
export class time{
    hour: number;
    minute: number;
    constructor(_hour:number,_minute:number){
        this.hour=_hour;
        this.minute=_minute;
    }
}
export class GiftCodeCalendarMinuteModel {
    key: string;
    begin: time;
    end: time;

    get beginMinute(): number {
        if (this.begin == null || this.begin == undefined || this.begin.hour <= 0) {
            return 0;
        }
        else {
            return this.begin.hour * 60 + this.begin.minute;
        }
    }
    get endMinute(): number {
        if (this.end == null || this.end == undefined || this.end.hour <= 0) {
            return 0;
        }
        else {
            return this.end.hour * 60 + this.end.minute;
        }
    }
}
