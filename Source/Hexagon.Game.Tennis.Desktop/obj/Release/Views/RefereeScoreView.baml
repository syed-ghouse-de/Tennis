   M S B A M L   `   `   `  ����   Hexagon.Game.Tennis.Desktop8    2Hexagon.Game.Tennis.Desktop.Views.RefereeScoreView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       � Title$  Referee Score Board��$��799.6��$	��768�� � WindowStartupLocation$ CenterScreen=�.��� $��0,10,24.6,0q�$	��778��$	��Top=� � RowDefinitions ��  � ColumnDefinitions �� �� $��0* ��� $��0* ��� $��0* ��� $��0* �.+��� $��Start��$
��Left=�$��643,207,0,0q�$	��Top=�$��75��$��19��{��� *IsMatchInProgress+���� *StartMatchCommand+�� -   $��cmbSets�� �� DisplayMemberPath$ Value�� �� SelectedValuePath$ Id��$
��Left=�$��
33,204,0,0q�$	��Top=�$	��120�� J� RenderTransformOrigin$ 
0.659,0.56&�$��22������ *
Score.Sets+ �� SelectedItem �� *SelectedSet+ �� Mode$ TwoWay=�&   System.Windows.Controls.DataGrid  -   	  Name$	 grdRefereeScore��
   CanUserAddRows
 .     AutoGenerateColumns .     CanUserDeleteRows .    CanUserReorderColumns .    CanUserSortColumns . $��33,231,18.8,44.6q����� *Games+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$
 Game��  $ Server's Initials��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Server.FirstName+4  .System.Windows.Controls.DataGridTemplateColumn  $ Points��   Width5  /System.Windows.Controls.DataGridLengthConverter$ *    CellTemplate �� .R��  �� ��  �� .+��� $��0��$��0������  �� Path$ Server.Point��� $��1��$��0������ $ Receiver.Point��� $��Match Name : ��$
��Left=�$��	33,25,0,0q�$	��Top=�$��26��$��88���� $
��Left=�$��23��$��
129,25,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��199������ *
Match.Name+$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=��� $��Court : ��$
��Left=�$��	33,53,0,0q�$	��Top=�$��26��$��48���� $
��Left=�$��23��$��
129,53,0,0q�$
 Wrap=�$	��Top=�$	��199������ *Match.Court+$ TwoWay=�$ PropertyChanged=��� $��Started On : ��$
��Left=�$��
423,24,0,0q�$	��Top=�$��26��$��77���� $
��Left=�$��23��$��
519,26,0,0q�$
 Wrap=�$	��Top=�$	��199��{�.  ���� *Score.Match.StartedOn+$ TwoWay=�$ PropertyChanged=��� $��Completed On : ��$
��Left=�$��
423,53,0,0q�$	��Top=�$��26��$��96���� $
��Left=�$��23��$��
519,53,0,0q�$
 Wrap=�$	��Top=�$	��199��{�.  ���� *Score.Match.CompletedOn+$ TwoWay=�$ PropertyChanged=��� $ 	FirstName��$ Id��$
��Left=�$��129,116,0,0q�$	��Top=�$	��199��$��22������ *	Players+ �� *	PlayerOne+$ TwoWay=��� $��Player 1 : ��$
��Left=�$��
33,116,0,0q�$	��Top=�$��26��$��61���� $ 	FirstName��$ Id��$
��Left=�$��519,116,0,0q�$	��Top=�$	��199��$��22������ *	Players+ �� *	PlayerTwo+$ TwoWay=��� $��Player 2 : ��$
��Left=�$��423,116,0,0q�$	��Top=�$��26��$��61���� $��Games Won : ��$
��Left=�$��
33,142,0,0q�$	��Top=�$��26��$��83���� $
��Left=�$��23��$��129,142,0,0q�$
 Wrap=�$	��Top=�$	��199��{�.  ���� *GamesWon[0]+$ TwoWay=�$ PropertyChanged=��� $��Games Won : ��$
��Left=�$��423,142,0,0q�$	��Top=�$��26��$��83���� $
��Left=�$��23��$��519,142,0,0q�$
 Wrap=�$	��Top=�$	��199��{�.  ���� *GamesWon[1]+$ TwoWay=�$ PropertyChanged=��� $��Best Of Sets : ��$
��Left=�$��	33,81,0,0q�$	��Top=�$��26��$��83���� $
��Left=�$��23��$��
129,81,0,0q�$
 Wrap=�$	��Top=�$	��199������ *Match.BestOfSets+$ TwoWay=�$ PropertyChanged=��� $
��Left=�$��129,183,0,0q�$
��Wrap=�$	��Top=����� *	Message+�� $��	Won By : ��$
��Left=�$��
423,81,0,0q�$	��Top=�$��26��$��96���� $
��Left=�$��23��$��
519,81,0,0q�$
 Wrap=�$	��Top=�$	��199��{�.  ���� *Score.Match.WonBy.FirstName+$ TwoWay=�$ PropertyChanged=�