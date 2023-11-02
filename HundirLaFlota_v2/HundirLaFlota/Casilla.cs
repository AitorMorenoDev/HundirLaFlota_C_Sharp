using System;
using System.Collections.Generic;
using System.Text;

namespace HundirLaFlota
{
    class Casilla
    {

        private int fila;
        private int columna;
        private char estado;
        private char agua = '.';
        private char barco = 'O';
        private char tocado= 'X';

        public Casilla(int fila, int columna)
        {
            this.fila = fila;
            this.columna = columna;
            estado = agua;
        }

        public int GetFila()
        {
            return fila;
        }
        public void SetFila(int fila)
        {
            this.fila = fila;
        }

        public int GetColumna()
        {
            return columna;
        }
        public void SetColumna(int columna)
        {
            this.columna = columna;
        }

        public char GetEstado()
        {
            return estado;
        }

        public void SetEstado(char estado)
        {
            this.estado = estado;
        }

        public char GetAgua()
        {
            return agua;
        }

        public void SetAgua()
        {
            SetEstado(agua);
        }

        public char GetBarco()
        {
            return barco;
        }
        public void SetBarco()
        {
            SetEstado(barco);
        }

        public char GetTocado()
        {
            return tocado;
        }
        public void SetTocado()
        {
            SetEstado(tocado);
        }
       
    }
}
