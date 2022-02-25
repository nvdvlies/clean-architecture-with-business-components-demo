//----------------------
// <auto-generated>
//     Generated using Demo.SignalrTypescript.Generator
// </auto-generated>
//----------------------
import { Injectable, Inject, InjectionToken } from '@angular/core';
import { HubConnection, IHttpConnectionOptions } from '@microsoft/signalr';
import { lastValueFrom, Subject } from 'rxjs';
import * as signalR from '@microsoft/signalr';
import { AuthService } from '@auth0/auth0-angular';

export const SIGNALR_BASE_URL = new InjectionToken<string>('SIGNALR_BASE_URL');

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  public hubConnection: HubConnection;

  constructor(
    @Inject(SIGNALR_BASE_URL) private readonly baseUrl: string,
    private readonly authService: AuthService
  ) {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(this.baseUrl + '/hub', {
        accessTokenFactory: () => lastValueFrom(this.authService.getAccessTokenSilently())
      } as IHttpConnectionOptions)
      .withAutomaticReconnect()
      .configureLogging(signalR.LogLevel.Warning)
      .build();

    this.authService.isAuthenticated$.subscribe((isAuthenticated) =>
      isAuthenticated ? this.connect() : this.disconnect()
    );
  }

  private connect(): void {
    this.hubConnection
      .start()
      .catch((err) => console.log('Error while starting connection: ' + err));
  }

  private disconnect(): void {
    if (this.hubConnection) {
      this.hubConnection
        .stop()
        .catch((err) => console.log('Error while terminating connection: ' + err));
    }
  }
}

export interface CustomerCreatedEvent {
  id: string;
  createdBy: string;
}

export interface CustomerUpdatedEvent {
  id: string;
  updatedBy: string;
}

export interface CustomerDeletedEvent {
  id: string;
  deletedBy: string;
}

@Injectable({
  providedIn: 'root'
})
export class CustomerEventsService {
  private customerCreated = new Subject<CustomerCreatedEvent>();
  private customerUpdated = new Subject<CustomerUpdatedEvent>();
  private customerDeleted = new Subject<CustomerDeletedEvent>();

  public customerCreated$ = this.customerCreated.asObservable();
  public customerUpdated$ = this.customerUpdated.asObservable();
  public customerDeleted$ = this.customerDeleted.asObservable();

  constructor(private signalRService: SignalRService) {
    this.signalRService.hubConnection.on('CustomerCreated', (id: string, createdBy: string) =>
      this.customerCreated.next({ id, createdBy })
    );
    this.signalRService.hubConnection.on('CustomerUpdated', (id: string, updatedBy: string) =>
      this.customerUpdated.next({ id, updatedBy })
    );
    this.signalRService.hubConnection.on('CustomerDeleted', (id: string, deletedBy: string) =>
      this.customerDeleted.next({ id, deletedBy })
    );
  }
}

export interface InvoiceCreatedEvent {
  id: string;
  createdBy: string;
}

export interface InvoiceUpdatedEvent {
  id: string;
  updatedBy: string;
}

export interface InvoiceDeletedEvent {
  id: string;
  deletedBy: string;
}

@Injectable({
  providedIn: 'root'
})
export class InvoiceEventsService {
  private invoiceCreated = new Subject<InvoiceCreatedEvent>();
  private invoiceUpdated = new Subject<InvoiceUpdatedEvent>();
  private invoiceDeleted = new Subject<InvoiceDeletedEvent>();

  public invoiceCreated$ = this.invoiceCreated.asObservable();
  public invoiceUpdated$ = this.invoiceUpdated.asObservable();
  public invoiceDeleted$ = this.invoiceDeleted.asObservable();

  constructor(private signalRService: SignalRService) {
    this.signalRService.hubConnection.on('InvoiceCreated', (id: string, createdBy: string) =>
      this.invoiceCreated.next({ id, createdBy })
    );
    this.signalRService.hubConnection.on('InvoiceUpdated', (id: string, updatedBy: string) =>
      this.invoiceUpdated.next({ id, updatedBy })
    );
    this.signalRService.hubConnection.on('InvoiceDeleted', (id: string, deletedBy: string) =>
      this.invoiceDeleted.next({ id, deletedBy })
    );
  }
}
