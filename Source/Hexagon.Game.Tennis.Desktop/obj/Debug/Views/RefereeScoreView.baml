   M S B A M L   `   `   `  ����  Hexagon.Game.Tennis.Desktop8    2Hexagon.Game.Tennis.Desktop.Views.RefereeScoreView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$  Referee Score Board��5   	   $��799.6��6%   $	��768��64    � WindowStartupLocation$ CenterScreen=�6@   .��5      � $��0,10,24.6,0q�6   $	��778��6    $	��Top=�6-    � RowDefinitions 5	   
   �� 5
       � ColumnDefinitions 5   
   �� 5      �� 5      $��0* �6   �� 5      $��0* �6   �� 5      $��0* �6   �� 5      $��0* �6   .+�5   
   �� $��Start��69   $
��Left=�6I   $��643,207,0,0q�6d   $	��Top=�6y   $��75��6�   $��19��6�   {�6   �� *IsMatchInProgress+��6�   �� *StartMatchCommand+6�   �� 5   
   -   5        $��cmbSets��5       �� DisplayMemberPath$ Value��5       �� SelectedValuePath$ Id��5      $
��Left=�5      $��
33,204,0,0q�6.   $	��Top=�6B   $	��120��6Z    J� RenderTransformOrigin$ 
0.659,0.56&�6f   $��22��6�   ��5   "   �� *
Score.Sets+ �� SelectedItem 5      �� *SelectedSet+ �� Mode$ TwoWay=�5   �   &   System.Windows.Controls.DataGrid  5   
   -   5        	  Name$	 grdRefereeScore��5      
   CanUserAddRows
 .     AutoGenerateColumns .     CanUserDeleteRows .    CanUserReorderColumns .    CanUserSortColumns . $��33,231,18.8,44.6q�5   '   ��5   C   �� *Games+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$
 Game��6%     5      $ Server's Initials��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6@   �� *Server.FirstName+4  .System.Windows.Controls.DataGridTemplateColumn  5      $ Points��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 69      CellTemplate 5       �� 5!      .R�5"      �  5#   "   �� 5$   &   �� 5%   &    5'   "   �� 5(   &   .+�5*   "   �� $��0��6,   $��0��69   ��6I   ��  �� Path$ Server.Point��� 5+   "   $��1��6,   $��0��69   ��6I   �� $ Receiver.Point�5,      5-      5/      51      �� 52   
   $��Match Name : ��6   $
��Left=�6(   $��	33,25,0,0q�6C   $	��Top=�6V   $��26��6n   $��88��6z   �� 53   
   $
��Left=�6`   $��23��6{   $��
129,25,0,0q�6�    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $	��199��6�   ��6   �� *
Match.Name+$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 54   
   $��Court : ��6   $
��Left=�6#   $��	33,53,0,0q�6>   $	��Top=�6Q   $��26��6i   $��48��6u   �� 55   
   $
��Left=�6a   $��23��6|   $��
129,53,0,0q�6�   $
 Wrap=�6�   $	��Top=�6�   $	��199��6�   ��6   �� *Match.Court+$ TwoWay=�$ PropertyChanged=�6�   �� 56   
   $��Started On : ��6   $
��Left=�6(   $��
423,24,0,0q�6C   $	��Top=�6W   $��26��6o   $��77��6{   �� 57   
   $
��Left=�6k   $��23��6�   $��
519,26,0,0q�6�   $
 Wrap=�6�   $	��Top=�6�   $	��199��6�   {�.  ��6   �� *Score.Match.StartedOn+$ TwoWay=�$ PropertyChanged=�6�   �� 58   
   $��Completed On : ��6   $
��Left=�6*   $��
423,53,0,0q�6E   $	��Top=�6Y   $��26��6q   $��96��6}   �� 59   
   $
��Left=�6m   $��23��6�   $��
519,53,0,0q�6�   $
 Wrap=�6�   $	��Top=�6�   $	��199��6�   {�.  ��6   �� *Score.Match.CompletedOn+$ TwoWay=�$ PropertyChanged=�6�   �� 5:   
   $ 	FirstName��5;      $ Id��5<      $
��Left=�5>      $��129,116,0,0q�6(   $	��Top=�6=   $	��199��6U   $��22��6a   ��5:      �� *	Players+ 5=      �� *	PlayerOne+$ TwoWay=�5>   a   �� 5?   
   $��Player 1 : ��6   $
��Left=�6&   $��
33,116,0,0q�6A   $	��Top=�6U   $��26��6m   $��61��6y   �� 5@   
   $ 	FirstName��5B      $ Id��5C      $
��Left=�5E      $��519,116,0,0q�6(   $	��Top=�6=   $	��199��6U   $��22��6a   ��5A      �� *	Players+ 5D      �� *	PlayerTwo+$ TwoWay=�5E   a   �� 5F   
   $��Player 2 : ��6   $
��Left=�6&   $��423,116,0,0q�6A   $	��Top=�6V   $��26��6n   $��61��6z   �� 5G   
   $��Games Won : ��6   $
��Left=�6'   $��
33,142,0,0q�6B   $	��Top=�6V   $��26��6n   $��83��6z   �� 5H   
   $
��Left=�6a   $��23��6|   $��129,142,0,0q�6�   $
 Wrap=�6�   $	��Top=�6�   $	��199��6�   {�.  ��6   �� *GamesWon[0]+$ TwoWay=�$ PropertyChanged=�6�   �� 5I   
   $��Games Won : ��6   $
��Left=�6'   $��423,142,0,0q�6B   $	��Top=�6W   $��26��6o   $��83��6{   �� 5J   
   $
��Left=�6a   $��23��6|   $��519,142,0,0q�6�   $
 Wrap=�6�   $	��Top=�6�   $	��199��6�   {�.  ��6   �� *GamesWon[1]+$ TwoWay=�$ PropertyChanged=�6�   �� 5K   
   $��Best Of Sets : ��6   $
��Left=�6*   $��	33,81,0,0q�6E   $	��Top=�6X   $��26��6p   $��83��6|   �� 5L   
   $
��Left=�6f   $��23��6�   $��
129,81,0,0q�6�   $
 Wrap=�6�   $	��Top=�6�   $	��199��6�   ��6   �� *Match.BestOfSets+$ TwoWay=�$ PropertyChanged=�6�   �� 5M   
   $
��Left=�6-   $��129,183,0,0q�6H   $
��Wrap=�6]   $	��Top=�6q   ��6   �� *	Message+6q   �� 5N   
   $��	Won By : ��6   $
��Left=�6$   $��
423,81,0,0q�6?   $	��Top=�6S   $��26��6k   $��96��6w   �� 5O   
   $
��Left=�6q   $��23��6�   $��
519,81,0,0q�6�   $
 Wrap=�6�   $	��Top=�6�   $	��199��6�   {�.  ��6   �� *Score.Match.WonBy.FirstName+$ TwoWay=�$ PropertyChanged=�6�   5P      5Q      