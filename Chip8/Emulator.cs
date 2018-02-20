using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chip8
{
    class Emulator
    {
        private ushort opcode;
        private byte[] memory = new byte[4096];
        private sbyte[] registers = new sbyte[16];

        private ushort pc;
        private ushort I;
        private sbyte[] V = new sbyte[16];

        private sbyte[] gfx = new sbyte[64 * 32];
        private sbyte delay_timer;
        private sbyte sound_timer;

        private ushort[] stack = new ushort[16];
        private ushort sp;

        private bool drawFlag;

        private byte[] keyArr = new byte[16];

        public delegate void DrawGraphics(sbyte[] gfx);
        public event DrawGraphics DrawGraphicsEvent;

        public bool Stop = false;

        public void SetKeys(char key)
        {
            switch(key)
            {
                case '1': keyArr[0] = 0x1; break;
                case '2': keyArr[0] = 0x1; break;
                case '3': keyArr[0] = 0x1; break;
                case '4': keyArr[0] = 0x1; break;
                case 'Q': keyArr[0] = 0x1; break;
                case 'W': keyArr[0] = 0x1; break;
                case 'E': keyArr[0] = 0x1; break;
                case 'R': keyArr[0] = 0x1; break;
                case 'A': keyArr[0] = 0x1; break;
                case 'S': keyArr[0] = 0x1; break;
                case 'D': keyArr[0] = 0x1; break;
                case 'F': keyArr[0] = 0x1; break;
                case 'Z': keyArr[0] = 0x1; break;
                case 'X': keyArr[0] = 0x1; break;
                case 'C': keyArr[0] = 0x1; break;
                case 'V': keyArr[0] = 0x1; break;
            }
        }

        public void Initialize()
        {
            pc = 0x200;
            opcode = 0;
            I = 0;
            sp = 0;

            for(int i = 0; i < 80; i++)
            {
                memory[i] = Fontset.Values[i];
            }
        }

        public void LoadGame(byte[] data)
        {
            int bufferSize = data.Length;
            for (int i = 0; i < bufferSize; ++i)
                memory[i + 512] = data[i];
        }

        public void EmulateCycle()
        {
            // Fetch opcode
            opcode = (ushort) (memory[pc] << 8 | memory[pc + 1]);

            // Decode opcode
            switch (opcode & 0xF000)
            {
                // Some opcodes //
                case 0xA000: 
                    I = (ushort) (opcode & 0x0FFF);
                    pc += 2;
                    break;
                case 0x0000:
                    switch (opcode & 0x000F)
                    {
                        case 0x0000: // 0x00E0: Clears the screen        
                            break;
                        case 0x000E: // 0x00EE: Returns from subroutine          
                            break;
                        default:
                            Console.WriteLine("Unknown opcode [0x0000]: 0x%X\n", opcode);
                            break;
                    }
                    break;
                case 0x2000:
                    stack[sp] = pc;
                    ++sp;
                    pc = (ushort) (opcode & 0x0FFF);
                    break;
                case 0x0004:
                    if (V[(opcode & 0x00F0) >> 4] > (0xFF - V[(opcode & 0x0F00) >> 8]))
                        V[0xF] = 1; //carry
                    else
                        V[0xF] = 0;
                    V[(opcode & 0x0F00) >> 8] += V[(opcode & 0x00F0) >> 4];
                    pc += 2;
                    break;
                case 0x0033:
                    memory[I] = (byte) (V[(opcode & 0x0F00) >> 8] / 100);
                    memory[I + 1] = (byte) ((V[(opcode & 0x0F00) >> 8] / 10) % 10);
                    memory[I + 2] = (byte) ((V[(opcode & 0x0F00) >> 8] % 100) % 10);
                    pc += 2;
                    break;
                case 0xD000:
                    {
                        ushort x = (ushort) (V[(opcode & 0x0F00) >> 8]);
                        ushort y = (ushort) (V[(opcode & 0x00F0) >> 4]);
                        ushort height = (ushort) (opcode & 0x000F);
                        ushort pixel;

                        V[0xF] = 0;
                        for (int yline = 0; yline < height; yline++)
                        {
                            pixel = memory[I + yline];
                            for (int xline = 0; xline < 8; xline++)
                            {
                                if ((pixel & (0x80 >> xline)) != 0)
                                {
                                    if (gfx[(x + xline + ((y + yline) * 64))] == 1)
                                        V[0xF] = 1;
                                    gfx[x + xline + ((y + yline) * 64)] ^= 1;
                                }
                            }
                        }

                        drawFlag = true;
                        pc += 2;
                    }
                    break;
                case 0xE000:
                    switch (opcode & 0x00FF)
                    {
                        // EX9E: Skips the next instruction 
                        // if the key stored in VX is pressed
                        case 0x009E:
                            if (keyArr[V[(opcode & 0x0F00) >> 8]] != 0)
                                pc += 4;
                            else
                                pc += 2;
                            break;
                    }
                    break;
                default:
                    Console.WriteLine("Unknown opcode: 0x%X\n", opcode);
                    break;
            }

            // Update timers
            if (delay_timer > 0)
                --delay_timer;

            if (sound_timer > 0)
            {
                if (sound_timer == 1)
                    Console.WriteLine("BEEP!\n");
                --sound_timer;
            }
        }

        public void Run()
        {
            while(!Stop)
            {
                EmulateCycle();

                if(drawFlag)
                {
                    DrawGraphicsEvent(gfx);
                }
            }
        }

    }
}
