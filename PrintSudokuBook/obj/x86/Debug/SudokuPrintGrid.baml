   M S B A M L   `   `   `  ����J  FPrintSudokuBook, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:PrintSudokuBook.TypeConvertersPrintSudokuBook.TypeConverters  5   	   %    PrintSudokuBook.SudokuPrintGrid   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   ;tc,clr-namespace:PrintSudokuBook.TypeConverters     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   >   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6{     � Title$  
MainWindow��6	   
 � Top$ 0��6    � Left$ 0��6$   $	��400��6�   $	��400��6�    � 	Resources 5      >   8PrintSudokuBook.TypeConverters.SudokuNumberTypeConverter   sntc��%   &	          5   
   6'   5      .��5	      � $
��Auto��6   $
��Auto��6    � ColumnDefinitions 5
   
   �� 5      $	��72* �6   �� 5      $
��306* �6    � RowDefinitions 5   
   �� 5      $
��530* �6   �� 5      $	��40* �6   .+�5   
   �� -   5        $��ItemsControl1��5      $��4��6K   ��6,   �� *Puzzle+��5      �� 5      .R�5      A� 5      ��5      �� 5      .R�5      �� 
���   ���61   ��  �� Path$ Tickness�._�5      �� .+�5      �� $��20(�6�   $��Center=�6�   $��Bottom=�6�   ��6(   �� $ Color���6J   �� $ Value� �� 	Converter# [  6�   5      5      5      5!      �� 5"   
   -   5        $��Print��5#   A   $��Print��5"      $��0��6!   $��1��61   $��20                 ��6>   $��1,1,1,1q�5#      $��Center=�6$   $��55��6N   6Y   5$      5%      