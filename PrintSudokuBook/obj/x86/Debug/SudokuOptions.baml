   M S B A M L   `   `   `  ����J  FPrintSudokuBook, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:PrintSudokuBook.TypeConvertersPrintSudokuBook.TypeConverters  5   	   #    PrintSudokuBook.SudokuOptions   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   ;tc,clr-namespace:PrintSudokuBook.TypeConverters     5   	     � Title$  SudokuPrintGrid��5   	   $	��300��6!   $	��300��6.    � 	Resources 5      <   6PrintSudokuBook.TypeConverters.DifficultyTypeConverter   dtc��%   &	          5   
   6%   5      .��5	      �  � RowDefinitions 5
   
   �� 5      $	��50* �6   �� 5      $
��176* �6   �� 5      $	��35* �6   .+�5   
   �� $��0��6   .+�5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $��Max Difficulty:  ��6   �� 5      -   5        $��DifficultyText��5      ��62   ��  �� ElementName$ DifficultySlider�� �� Path$ Value� �� 	Converter# [  5      �� 5      -   5        $��DifficultySlider��5      $��5 5 40 5q�6-    �� IsSnapToTickEnabled .  � Minimum$ 1Q�6�   	 � Maximum$	 3Q�6�   
 �� TickPlacement$
 TopLeft=�6�    � Value 6?   �� *
Difficulty+ �� Mode$ TwoWay=�6�   5      �� 5   
   $ 
Horizontal=�6   $��Center=�6.   $��2��6K   .+�5      �� -   5        $��Go��5   ]   $��Go��6   $��23��6"   $��0 0 20 0q�6.   $��Center=�6@   $	��Top=�6g   $��75��6   6�   �� 5      -   5        $��CloseButton��5   a   $��Close��6   $��23��6%   $��	20 0 0 0 q�61   $��Center=�6D   $	��Top=�6t   $��75��6�   6�   5      5      5      