   M S B A M L   `   `   `  ����L  HProjet File Rouge, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:Projet_File_Rouge.ViewsProjet_File_Rouge.Views  5      .    (Projet_File_Rouge.Views.GlobalCenterView   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:Projet_File_Rouge.Views     5        � 	Resources  5
         left 	 right�� 5   
   %s   &	        &	 /     �� 5      $��../Img/arrow_left.png��6!   �� 5      $��../Img/arrow_right.png��6"   5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��Retour��6   $��Right=�6/   $��Center=�6K   $��12q�6f   $��5q�6r   $��20(�6~   ��6�   �� *NavigateMainMenuCommand+� 5   
   $��1��6   $��Center=�6    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   .+�5"      �� -   5        $��0��5"      $��40��6%   $��40��60   #��[  6<   
������ 
������ 6�   �� 5$      $��1��6   $��12q�6(    �� Padding$ 5q�64   $��20(�6@   $��Center=�6N   ��6k   �� *
Pagination+ �� UpdateSourceTrigger$ PropertyChanged=��� 5&      -   5        $��2��5&      $��40��6%   $��40��60   #��[ 6<   
������ 
������ 6�   5(      � 5*   
   $��2��6   $��Stretch=�6    5+      �� 5,      $��* �6#   �� 5-      $��* �6#   �� 5.      $��* �6#   �� 5/      $��* �6#   �� 50      $
��Auto �6#   �� 51      $
��Auto �6#   .+�54      � $��0��6   $��Center=�6#    55      �� 56      $
��Auto �6$   �� 57      $
��Auto �6$   .+�5:      �� $��0��6   $��12q�6)   $ 5q�65   $��20(�6A   $��Center=�6O   $
��Date��6l   (  "System.Windows.Controls.DatePicker  5;      $��1��6   $��12q�6*   $��5q�66   $��20(�6B   $��Center=�6P      SelectedDate 6m   �� *Date+$ PropertyChanged=�5<      � 5>      $��1��6   $��Center=�6#    5?      �� 5@      $
��Auto �6$   �� 5A      $
��Auto �6$   .+�5D      �� $��0��6   $��12q�6)   $ 5q�65   $��20(�6A   $��Center=�6O   $��Statut��6l   � 5E      $��1��6   $��Center=�6$   $��Center=�6?    5F      �� 5G      $
��Auto �6+   �� 5H      $
��Auto �6+   .+�5K      �� $��0��6    $��12q�60   $ 5q�6<   $��20(�6H   $��Aucune Selection��6V   ��5L      �� 5M       �� 
TargetType" ��6$   .�5N   "   ��  �� Property � y� �� Value$ Visible=�6?   	 �� Triggers	 5O   "   �� 5P   &   
 �� Binding
 62   �� *StepListNullVisibility+$ PropertyChanged=� �� Value True6�   .a�5Q   *   ��  � y�$ 	Collapsed=�6G   5R   '   5T      5V      �� 5W      $��1��6   $��12q�6/   $��5q�6;   $��20(�6G   ��6U   �� *
StepList+ �� SelectedIndex 6v   �� *Step+$ PropertyChanged=�5X      5Y      � 5[      $��2��6   $��Center=�6#    5\      �� 5]      $
��Auto �6$   �� 5^      $
��Auto �6$   .+�5a      �� $��0��6   $��12q�6)   $ 5q�65   $��20(�6A   $��Center=�6O   $��
Technicien��6l   � 5b      $��1��6   $��Center=�6$   $��Center=�6?    5c      �� 5d      $
��Auto �6+   �� 5e      $
��Auto �6+   .+�5h      �� $��0��6    $��12q�60   $ 5q�6<   $��20(�6H   $��Aucune Selection��6V   ��5i      �� 5j      " ��6$   .�5k   "   ��  � y�$ Visible=�6?   	 5l   "   �� 5m   &   
 62   �� *UserListNullVisibility+$ PropertyChanged=� True6�   .a�5n   *   ��  � y�$ 	Collapsed=�6G   5o   '   5q      5s      �� 5t      $��1��6   $��12q�6/   $��5q�6;   $��20(�6G   ��6U   �� *
UserList+ 6v   �� *UserId+$ PropertyChanged=�5u      5v      � 5x      $��3��6   $��Center=�6#    5y      �� 5z      $
��Auto �6$   �� 5{      $
��Auto �6$   .+�5~      �� $��0��6   $��12q�6)   $ 5q�65   $��20(�6A   $��Center=�6O   $��
Nom Client��6l   �� 5      $��1��6   $��12q�6'   $��5q�63   $��20(�6?   $��Stretch=�6M   ��6k   �� *
ClientName+$ PropertyChanged=�5�      �� 5�      -   5        $��4��5�      $��12q�6%   $��5q�61   $��20(�6=   $��Center=�6K   $��Center=�6h   $��Filtrer��6�   6�   �� 5�      -   5        $��5��5�      $��12q�6%   $��5q�61   $��20(�6=   $��Center=�6K   $��Center=�6h   $��Annuler Filtre��6�   6�   5�      &   System.Windows.Controls.DataGrid  5�   
   -   5        $��3��5�      $��12q�6    $��5q�6,   $��20(�68   $��Center=�6F      MinRowHeight$ 40Q�6c      AutoGenerateColumns$ False��6u      CanUserResizeColumns$ False��6�      
IsReadOnly$
 True��6�   ��6�   �� *RedWireList+ �� Mode$ OneWay=�$ PropertyChanged=�   Columns 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6%      Header$ Prise en charge��6/   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6H   �� *InChargeDateFormated+  5�      $ * 6%   $ Début réparation��6/    6I   �� *RepairStartDateFormated+  5�      $ * 6%   $ Fin réparation��6/    6G   �� *RepairEndDateFormated+  5�      $ * 6%   $ Numéro Prise en Charge��6/    6O   �� *Client+  5�      $ * 6%   $ Type de Support��6/    6H   �� *Type+  5�      $ * 6%   $ Model du Support��6/    6I   �� *Model+  5�      $ * 6%   $ Statut��6/    6?   �� *ActualState+  5�      $ * 6%   $ Réparateur��6/    6C   �� *ActualRepairMan.Name+  5�      $ * 6%   $ 
Nom Client��6/    6C   �� *
ClientName+5�      5�      5�      