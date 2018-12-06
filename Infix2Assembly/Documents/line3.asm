code segment

push 01h
push 09h

; addition
pop ax
pop bx
add ax,bx ; result in ax
push ax
push 0ah

; division
pop bx
pop ax
xor dx,dx ; clear dx, assume 16 bit operand
div bx ; result in ax
push ax

pop ax
call print_word
int 020h ; exit to dos interrupt

print_word:
push ax; put the value of ax on top of stack
shr ax, 8 ; right shift value of ax 8 bits (get leftmost two nibbles)
call print_byte
pop ax; restore value of ax
push ax; put the value of ax on top of stack
and ax, 0ffh ; get last 8 bits of ax (get rightmost two nibbles)
call print_byte
pop ax; restore value of ax
ret ; return

print_byte:
push ax; put the value of ax on top of stack
shr al, 4 ; right shift value of al 4 bits(get left nibble)
call printchar
pop ax; restore value of ax
and al, 0fh ; get last 4 bits of al(get right nibble)
call printchar
ret ; return

printchar:
add al, 030h ; add hex value 30 to convert to ascii(0=30h, 9=39h)
cmp al, 039h ; compare value with ascii code for 9
jle printchar_letter; jump if value is less than or equal(ZF, CF)
add al, 07h ; add 7 to convert numbers bigger than 9 to ascii(A=41h)

printchar_letter:
mov dl, al; move character to write to dl
mov ah, 02h ; set int21h mode to write character to stdout
int 021h ; execute interrupt
ret ; return

code ends