   M S B A M L   `   `   `  ���� E  ASudokuGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:SudokuGame.TypeConvertersSudokuGame.TypeConverters       SudokuGame.SudokuPrintGrid   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     6tc'clr-namespace:SudokuGame.TypeConverters     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       � Title$  Print Sudoku Game��
 � Top$ 0�� � Left$ 0��$	��500��$	��500�� � 	Resources 9   3SudokuGame.TypeConverters.SudokuNumberTypeConverter   sntc��%   &	          .��� $
��Auto��$
��Auto�� � ColumnDefinitions �� $	��72* ��� $
��306* � � RowDefinitions �� $
��530* ��� $	��40* �.+��� -   $��ItemsControl1��$��4������ *Puzzle+���� .R�A� $��
15,15,15,1q����� .R��� 
���   �����  �� Path$ Tickness�._��� .+��� $��20(�$��Center=�$��Bottom=�$��2,4,2,2q����� $ Color����� $ Value� �� 	Converter# [  �� -   $��Print��$��Print��$��0��$��1��$��20                 ��$��1,1,1,1q�$��Center=�$��55���� -   $��
PuzzleInfo��$��1��$��1��$��12(�$
��Auto��$
��Left=�$��2,2,2,2q�$��Center=����� $ Puzzle.PuzzleInfo�