   M S B A M L   `   `   `  ����D  @movieMVVM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:movieMVVM.ViewmovieMVVM.View  5   	   9!clr-namespace:movieMVVM.ViewModelmovieMVVM.ViewModel  5   	       movieMVVM.View.UserView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   .localclr-namespace:movieMVVM.View     5   	   7	viewModel!clr-namespace:movieMVVM.ViewModel     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  UserView��5   	   $��340.063��6   $��509.82��6+    � SizeToContent$ Width=�5   	   .��5      � $��0,0,2,4q�6    � ColumnDefinitions 5	   
   �� 5
      $
��157* �6   �� 5      $	��93* �6   �� 5      $
��Auto �6   .+�5   
   � $
��Left=�6   $	��254��6*   $��	38,30,0,0q�67   $	��Top=�6J   $	��427��6b   $��2��6n    5      �� 5      $��3* �6#   �� 5      $��4* �6#   .+�5      �� -   5         ��Name$ Movies��5   <   $��-10,10,10,-10q�6L   $��2��6c   ��6   �� *
DBMovies+��5      �� 5      .R�5      ��  �� Orientation$ Vertical=�6%   .+�5      �� $	��100��6?   $	��100��6L   ��6$   �� *	ImgPath+6L   �� 5      $��
Buy Ticket��6%   ��6;   ��  �� ElementName$ Movies�� �� Path$" DataContext.BuyTicketCommand���6�   �� *ID+�� 5      $ 
Horizontal=�6)   .+�5   "   �� $��Name: ��6,   �� 5   "   ��6,   �� *Name+5      �� 5      $ 
Horizontal=�6)   .+�5   "   �� $��Tickets Sold: ��6,   �� 5   "   ��6,   �� *TicketsSold+5       �� 5!      $ 
Horizontal=�6)   .+�5"   "   �� $��Tickets Remaining: ��6,   �� 5#   "   ��6,   �� *TicketsRemaining+5$      5%      5&      5(      5)      �� 5*   
   $
��Left=�6   $��	161,9,0,0q�6/   $
��Wrap=�6B   $��Movies Playing Right Now��6V   $	��Top=�6v   $	��153��6�   $
��Bold"�6�   5/      50      