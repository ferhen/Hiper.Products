// core
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

// models
import { IAppConfig } from './app-config.model';

@Injectable({
    providedIn: 'root'
})
export class AppConfig {
    static settings: IAppConfig;
    static endpoints: {};

    constructor(private readonly http: HttpClient) { }

    public static getPath(type: 'api' | 'hubs', ...params: string[]): string {
        const baseUrl = AppConfig.settings.apiServer.baseUrl;
        const endpointConfig = AppConfig.endpoints;
        return baseUrl + `${type}/` + params.reduce((o, key) => o[key], endpointConfig[type]);
    }

    public load() {
        const appConfigJsonFile = `assets/config/config.json`;
        const endpointJsonFile = `assets/config/endpoints.json`;

        return new Promise<void>(async (resolve, reject) => {
            try {
                AppConfig.settings = await this.http.get<IAppConfig>(appConfigJsonFile).toPromise();
                AppConfig.endpoints = await this.http.get<any>(endpointJsonFile).toPromise();
                resolve();
            } catch (err) {
                reject(`Could not load app config - '${err}'`);
            }
        });
    }
}
