   M S B A M L   `   `   `  ����J  FPrintSudokuBook, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:PrintSudokuBook.TypeConvertersPrintSudokuBook.TypeConverters  5   	   &     PrintSudokuBook.SudokuPrintGrid2   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   ;tc,clr-namespace:PrintSudokuBook.TypeConverters     5   	     � Title$  
MainWindow��5   	   $	��900��6   $	��700��6(   
 � Top$ 0��65    � Left$ 0��6=    � 	Resources 5      >   8PrintSudokuBook.TypeConverters.SudokuNumberTypeConverter   sntc��%   &	          5   
   6'   5      .��5	      �  � ColumnDefinitions 5
   
   �� 5      $	��93* �6   �� 5      $	��92* �6   �� 5      $
��503* �6   �� 5      $
��190* �6    � RowDefinitions 5   
   �� 5      $
��530* �6   �� 5      $	��31* �6   .+�5   
   �� -   5        $��ItemsControl1��5      $��4��6K   ��6,   �� *Puzzle+��5      �� 5      .R�5      A� 5      ��5      �� 5      .R�5      �� 
���   ���61   ��  �� Path$ Tickness�._�5      �� .+�5      �� ��6(   �� $ Color���5   "   `� 5    &    �� StringFormat$D >{0}
Solution: {1}
Guesses: {2}
Avoid Uncover: {3}
Highest: {4}��63   .1�5!   *   �� $ Value�62    �� 	Converter# [  6?   �� 5"   *   $ SolutionValue�62   �� 5#   *   $ .�62   �� 5$   *   $ AvoidUncover�62   �� 5%   *   $ HighestStep�62   5&   '   5(      5)      5*      5+      5-      �� 5.   
   -   5        $
��Next��5.   d   $
��Next��6   $��1��6    $��0��6-   $��23��6=   $
��Left=�6I   $��75��6p   $	��7,7q�6�   �� 5/   
   -   5        $
��Back��5/   v   $
��Back��6   {�.  $��1��62   $��1��6?   $��23��6O   $
��Left=�6[   $��75��6�   $��7,7,0,7q�6�   �� 50   
   -   5        $��Status��50      $��1��6"   $��2��6/   $��
1, 1, 1, 1q�6c   $
��Left=�6w   $��Center=�6�   ��6?   �� $ Puzzle.Status�6�   �� 51   
   -   5        $��Print��51   v   $��Print��6   $��3��6!   $��1��61   $��23��6>   $
��Left=�6J   $��1,1,1,1q�6e   $��75��6�   6�   52      53      