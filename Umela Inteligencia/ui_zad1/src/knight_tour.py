# sachovnicu prezentujeme riadkami a stlpcami
# rozmer sachovnice je dynamicky
import sys

import knight
import sys
import utils
import numpy as np
from random import randint


if len(sys.argv) == 2:
    size = int(sys.argv[1])
    s_x = randint(0, size - 1)
    s_y = randint(0, size - 1)
elif len(sys.argv) == 4:
    size = int(sys.argv[1])
    s_x = int(sys.argv[2])
    s_y = int(sys.argv[3])
elif len(sys.argv) == 3:
    size = randint(5, 6)
    s_x = int(sys.argv[1])
    s_y = int(sys.argv[2])
else:
    size = randint(5, 6)
    s_x = randint(0, size - 1)
    s_y = randint(0, size - 1)
    

print("Size: {}".format(size))
print("Starting position: x: {}, y: {}".format(s_x, s_y))
def rec_find_path(cur_x, cur_y, jump, chessboard, knight, moves):

    # vsetky policka su oznacene, uspesne skoncenie programu
    if jump == size**2+1:
        return True
    # vypnutie programu ak uz sme v moc velkej hlbke
    if knight.get_steps() >= 10000000:
        print("Solution could not be found")
        sys.exit(1)

    #postupne prejdem vsetky moznosti pohybu
    for num in range(8):
        move_x, move_y = knight.get_move(num)
        new_x = cur_x + move_x
        new_y = cur_y + move_y

        #ak je policko bezpecne oznacim ho
        if utils.is_move_save(new_x, new_y, chessboard, size):
            
            moves.append((move_x, move_y))
            chessboard[new_x][new_y] = jump
            knight.steps_up()
            #rekurzivne ponorenie s novymi suradnicami 
            if (rec_find_path(new_x, new_y, (jump+1), chessboard, knight, moves)):
                return True
            #ak sa rekurzia pride az sem zmenim policko na nenavstivene
            chess[new_x][new_y] = 0
            #znizim ponorenie o jedno
            knight.steps_down()
            #vyhodim krok zo stack-u
            moves.pop()

    return False
            

knight = knight.Knight()
chess = utils.create_chessboard(size, size)

#nastavenie prveho skoku kona
chess[s_x][s_y] = 1
moves = []
#rekurzia zacne druhym skokom
exists = rec_find_path(s_x, s_y, 2 ,chess, knight, moves)
if exists:
    print("")
    print("Solutuion exists:")
    print(np.matrix(chess))
    print("")
    print("Moves which horse has to take:")
    print(moves[:int(size**2/2)])
    print(moves[int(size**2/2 + 1):])
else:
    print("Solution does not exist")
