   M S B A M L   `   `   `  ���� E  ASudokuGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:SudokuGame.TypeConvertersSudokuGame.TypeConverters      SudokuGame.SudokuOptions   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     6tc'clr-namespace:SudokuGame.TypeConverters       � Title$  Sudoku Game��$	��300��$	��300�� � 	Resources 7   1SudokuGame.TypeConverters.DifficultyTypeConverter   dtc��%   &	          .���  � RowDefinitions �� $	��50* ��� $	��35* ��� $
��141* ��� $	��35* �.+��� $��0��.+���  �� Orientation$ 
Horizontal=�.+��� $��Max Difficulty:  ���� -   $��DifficultyText������  �� ElementName$ DifficultySlider�� �� Path$ Value� �� 	Converter# [  �� -   $��DifficultySlider��$��5 5 40 5q� �� IsSnapToTickEnabled .  � Minimum$ 1Q�	 � Maximum$	 3Q�
 �� TickPlacement$
 TopLeft=� � Value �� *
Difficulty+ �� Mode$ TwoWay=��� $ 
Horizontal=�$��Center=�$��3��.+��� -   $��Go��$��Go��$��23��$��0 0 20 0q�$��Center=�$	��Top=�$��75���� -   $��CloseButton��$��Close��$��23��$��	20 0 0 0 q�$��Center=�$	��Top=�$��75���� $ 
Horizontal=�$
��Left=�$��1��.+�	� -   $��radioButtonPrint��$��Print�� d� 	IsChecked$
 TrueY�$��1��$��16��$
��Left=�$��5,5,5,5q�$��Center=�	� -   $��radioButtonPlay��$
��Play��$��1��$��16��$
��Left=�$��5,5,5,5q�$��Center=�