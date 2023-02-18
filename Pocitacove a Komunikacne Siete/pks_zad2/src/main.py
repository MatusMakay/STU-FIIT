import sys

from classes.receiver import Receiver
from classes.sender import Sender
from exc.switch import SwitchRoles
from utils.utils import create_sockets


if len(sys.argv) == 2:
    my_port = int(sys.argv[1])
else:
    my_port = 20000

sen = Sender("s")
rec = Receiver("r")
count = 0

def receiver(obj, is_socket_set, flag_print, flag_switch, count):
    print("Receiver IP and PORT:")
    try:
        if not is_socket_set:
            rec.set_port(my_port)
            is_socket_set = True
            sockets = create_sockets(rec)

        else:
            sockets = obj.get_sockets()

        rec.set_my_sockets(sockets)
        rec.set_my_ip_port()
        rec.print_info(flag_print, rec)
        rec.start(flag_switch)

    except SwitchRoles as e:
            other_side = e.value
            sender(rec, is_socket_set, "rsw", True, count, other_side)


def sender(obj, is_socket_set, flag_print, flag_switch, count, other_side):
    print("Sender IP and PORT:")
    try:
        if not is_socket_set:
            sen.set_port(my_port)
            sockets = create_sockets(sen)

            is_socket_set = True
        else:
             sockets = obj.get_sockets()

        if(other_side != {}):
            sen.set_other_side(other_side)

        sen.set_my_sockets(sockets)
        sen.set_my_ip_port()
        sen.print_info(flag_print, sen)
        sen.start(flag_switch, count)
        
    except SwitchRoles:
        receiver(sen, is_socket_set, "ssw", True, count)
    

def main():

    print("".center(50, '-'))
    print("Komunikator".center(50, '-'))
    print("".center(50, '-'))
    print("")
    print("Prijimac/Vysielac \np/v")
    count = 0
    
    is_socket_set = False

    while(True):
        inp = input()
        if inp == 'p':
            receiver({}, is_socket_set, "r", False, count)        
        elif inp == 'v':
            sender({}, is_socket_set, "s", False, count, {})

main()