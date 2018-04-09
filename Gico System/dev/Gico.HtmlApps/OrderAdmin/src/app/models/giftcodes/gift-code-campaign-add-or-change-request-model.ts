import { GiftCodeCalendarViewModel } from './gift-code-calendar-view-model'
import { GiftCodeConditionViewModel } from './gift-code-condition-view-model'
export class GiftCodeCampaignAddOrChangeRequestModel {
    id: string;
    name: string;
    notes: string;
    beginDate: string;
    endDate: string;
    allowPaymentOnCheckout: boolean;
    shardId: number;
    version: number;
    calendars: GiftCodeCalendarViewModel[];
    conditions: GiftCodeConditionViewModel[];
    calendarMode:number;
}
