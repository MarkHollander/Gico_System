export class ResultModel {
    status: number;
    message: string;
    messages: string[];
}

export class KeyValueModel {
    value: string;
    text: string;
    checked: boolean;
}

export class BaseModel {
    id: string;
    code: string;
    languageId: string;
}
