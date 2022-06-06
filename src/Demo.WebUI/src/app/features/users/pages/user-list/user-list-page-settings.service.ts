import { Injectable } from '@angular/core';
import { SortDirection } from '@angular/material/sort';
import { UserSortColumn } from './user-table-data.service';

export class UserListPageSettings {
  public pageSize: number | undefined;
  public sortColumn: UserSortColumn | undefined;
  public sortDirection: SortDirection | undefined;
}

@Injectable()
export class UserListPageSettingsService {
  private key = UserListPageSettings.name;

  private _settings: UserListPageSettings | undefined;

  public get settings(): UserListPageSettings {
    if (this._settings) {
      return this._settings;
    }
    var json = localStorage.getItem(this.key);
    this._settings = json ? this.tryParse(json) : new UserListPageSettings();
    return this._settings;
  }

  public update(settings: Partial<UserListPageSettings>): void {
    Object.assign(this.settings, settings);
    localStorage.setItem(this.key, JSON.stringify(this.settings));
  }

  private tryParse(json: string): UserListPageSettings {
    try {
      return JSON.parse(json) as UserListPageSettings;
    } catch (error) {
      console.error(error);
      return new UserListPageSettings();
    }
  }
}