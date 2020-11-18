import { NgModule } from "@angular/core";
import { CommonModule } from '@angular/common';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { MatBadgeModule } from '@angular/material/badge'; 
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card'; 
import { MatCheckboxModule } from '@angular/material/checkbox'; 
import { MatDialogModule } from '@angular/material/dialog'; 
import { MatExpansionModule } from '@angular/material/expansion';
import { MatFormFieldModule } from '@angular/material/form-field'; 
import { MatGridListModule } from '@angular/material/grid-list'; 
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input'; 
import { MatListModule } from '@angular/material/list'; 
import { MatMenuModule } from '@angular/material/menu'; 
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner'; 
import { MatSelectModule } from '@angular/material/select';
import { MatSliderModule } from '@angular/material/slider';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatTableModule } from '@angular/material/table'; 
import { MatTabsModule } from '@angular/material/tabs';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatTooltipModule } from '@angular/material/tooltip';

@NgModule({

  imports: [
   CommonModule,
   DragDropModule,
   MatBadgeModule,
   MatButtonModule,
   MatCardModule,
   MatCheckboxModule,
   MatDialogModule,
   MatExpansionModule,
   MatFormFieldModule,
   MatGridListModule,
   MatIconModule,
   MatInputModule,
   MatListModule,
   MatMenuModule,
   MatPaginatorModule,
   MatProgressBarModule,
   MatProgressSpinnerModule,
   MatSelectModule,
   MatSliderModule,
   MatSnackBarModule,
   MatTableModule,
   MatTabsModule,
   MatToolbarModule,
   MatTooltipModule
],

  exports: [
      CommonModule,
      DragDropModule,
      MatBadgeModule,
      MatButtonModule,
      MatCardModule,
      MatCheckboxModule,
      MatDialogModule,
      MatExpansionModule,
      MatFormFieldModule,
      MatGridListModule,
      MatIconModule,
      MatInputModule,
      MatListModule,
      MatMenuModule,
      MatPaginatorModule,
      MatProgressBarModule,
      MatProgressSpinnerModule,
      MatSelectModule,
      MatSliderModule,
      MatSnackBarModule,
      MatTableModule,
      MatTabsModule,
      MatToolbarModule,
      MatTooltipModule
   ],
})

export class CustomMaterialModule { }