import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { OptionsService } from '../services/options.service';

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.css']
})
export class MessageComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<MessageComponent>, private optionsService: OptionsService) {}

  subject: string = "";
  body: string = "Ceci est un test";
  previewMessage: string[];
  selectedRecipient: string;
  recipientOptions: { [id: string]: string; } = {};
  isWriting: boolean;
  isFormatting: boolean;

  ngOnInit(): void {
    this.optionsService.getOptions("Opponent", null).subscribe(l => {
      this.recipientOptions = l;
    });
    this.isWriting = true;
  }

  back(): void {
    this.isWriting = true;
    this.isFormatting = false;
  }

  preview(): void {
    this.previewMessage = this.body.split('\n');
    this.isWriting = false;
    this.isFormatting = true;
  }

  send(): void {
    this.dialogRef.close();
  }
}
