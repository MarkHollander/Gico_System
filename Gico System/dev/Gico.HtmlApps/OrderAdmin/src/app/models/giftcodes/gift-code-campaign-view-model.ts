import { GiftCodeCalendarViewModel } from './gift-code-calendar-view-model'
import { GiftCodeConditionViewModel } from './gift-code-condition-view-model'

export class GiftCodeCampaignViewModel {
    id: string;
    name: string;
    notes: string;
    beginDate: string;
    endDate: string;
    message: string;
    allowPaymentOnCheckout: boolean;
    approvedDate: string;
    approvedUid: string;
    status: number;
    shardId: number;
    version: number;
    calendars: GiftCodeCalendarViewModel[];
    conditions: GiftCodeConditionViewModel[];
    calendarMode:number;
}
