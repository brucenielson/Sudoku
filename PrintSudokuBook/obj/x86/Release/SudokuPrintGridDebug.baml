   M S B A M L   `   `   `  ���� E  ASudokuGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:SudokuGame.TypeConvertersSudokuGame.TypeConverters  !    SudokuGame.SudokuPrintGrid2   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     6tc'clr-namespace:SudokuGame.TypeConverters       � Title$  
MainWindow��$	��900��$	��700��
 � Top$ 0�� � Left$ 0�� � 	Resources 9   3SudokuGame.TypeConverters.SudokuNumberTypeConverter   sntc��%   &	          .���  � ColumnDefinitions �� $	��93* ��� $	��92* ��� $
��503* ��� $
��190* � � RowDefinitions �� $
��530* ��� $	��31* �.+��� -   $��ItemsControl1��$��4������ *Puzzle+���� .R�A� ���� .R��� 
���   �����  �� Path$ Tickness�._��� .+��� ���� $ Color���`�  �� StringFormat$D >{0}
Solution: {1}
Guesses: {2}
Avoid Uncover: {3}
Highest: {4}��.1��� $ Value� �� 	Converter# [  �� $ SolutionValue��� $ .��� $ AvoidUncover��� $ HighestStep��� -   $
��Next��$
��Next��$��1��$��0��$��23��$
��Left=�$��75��$	��7,7q��� -   $
��Back��$
��Back��{�.  $��1��$��1��$��23��$
��Left=�$��75��$��7,7,0,7q��� -   $��Status��$��1��$��2��$��
1, 1, 1, 1q�$
��Left=�$��Center=����� $ Puzzle.Status��� -   $��Print��$��Print��$��3��$��1��$��23��$
��Left=�$��1,1,1,1q�$��75��