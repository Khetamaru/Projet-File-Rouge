   M S B A M L   `   `   `  ����L  HProjet File Rouge, Version=1.1.4.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:Projet_File_Rouge.ViewModelProjet_File_Rouge.ViewModel  5      0    *Projet_File_Rouge.Views.DocumentCenterView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ?	viewmodel)clr-namespace:Projet_File_Rouge.ViewModel     5      .��5
      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��Retour��6   $��Right=�6/   $��Center=�6K   $��12q�6f   $��5q�6r   $��20(�6~   ��6�   �� *NavigateRedWireCommand+&   System.Windows.Controls.DataGrid  5   
   -   5        $��1��5      $��12q�6    $��5q�6,   $��20(�68   $��Center=�6F      MinRowHeight$ 40Q�6c      AutoGenerateColumns$ False��6u      CanUserResizeColumns$
 True��6�      
IsReadOnly$
 True��6�   ��6�   �� *DocumentList+ �� Mode$ OneWay=� �� UpdateSourceTrigger$ PropertyChanged=�   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6%   	   Header$	 Numéro EBP��6/   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6C   �� *
Document+5      5      5      