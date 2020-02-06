﻿using System;
using Servicios.Colecciones.Nodos;

namespace Servicios.Colecciones.TADS
{
    public class clsTADSimpleEnlazado<Tipo>:clsTAD<Tipo> where Tipo: IComparable
    {
        #region Atributos
        private clsNodoSimpleEnlazado<Tipo> atrNodoPrimero;
        private clsNodoSimpleEnlazado<Tipo> atrNodoUltimo;
        #endregion
        #region Métodos
        #region Auxiliares
        protected bool ReversarNodos()
        {
            if (!EstaVacia())
            {
                clsNodoSimpleEnlazado<Tipo> varNodoNuevo = atrNodoUltimo;
                clsNodoSimpleEnlazado<Tipo> varNodoActual;
                clsNodoSimpleEnlazado<Tipo> varNodoAuxiliar;
                varNodoAuxiliar = varNodoNuevo;
                for (int varIndice1 = 1; varIndice1 < atrLongitud; varIndice1++)
                {
                    varNodoActual = atrNodoPrimero;
                    for (int varIndice2 = 1; varIndice2 < atrLongitud - varIndice1; varIndice2++)
                        varNodoActual = varNodoActual.darSiguiente();
                    if(varIndice1 == atrLongitud - 1)
                    {
                        varNodoAuxiliar.ponerSiguiente(null);
                        atrNodoUltimo = atrNodoPrimero;
                        atrNodoPrimero = varNodoNuevo;
                        return true;
                    }
                    else
                    {
                        varNodoAuxiliar.ponerSiguiente(varNodoActual);
                        varNodoAuxiliar = varNodoAuxiliar.darSiguiente();
                    }
                }

            }
            return false;
        }
        #endregion
        #region CRUDS
        protected override bool InsertarEn(int prmIndice, Tipo prmItem)
        {
            clsNodoSimpleEnlazado<Tipo> varNodoNuevo = new clsNodoSimpleEnlazado<Tipo>(prmItem);
            if (EstaVacia())
            {
                atrNodoPrimero = varNodoNuevo;
                atrNodoUltimo = varNodoNuevo;
                atrNodoPrimero.ponerSiguiente(atrNodoUltimo);
                atrLongitud++;
                return true;
            }
            if (prmIndice==0)
            {
                varNodoNuevo.ponerSiguiente(atrNodoPrimero);
                atrNodoPrimero = varNodoNuevo;
                atrLongitud++;
                return true;
            }
            if (prmIndice == atrLongitud)
            {
                atrNodoUltimo.ponerSiguiente(varNodoNuevo);
                atrNodoUltimo = varNodoNuevo;
                atrLongitud++;
                return true;
            }
            if (IrIndice(prmIndice - 1))
            {  
                varNodoNuevo.ponerSiguiente(atrNodoActual.darSiguiente());
                atrNodoActual.ponerSiguiente(varNodoNuevo);
                atrLongitud++;
                return true;
            }
            return false;
        }
        protected override bool ExtraerEn(int prmIndice, ref Tipo prmItem)
        {
            clsNodoSimpleEnlazado<Tipo> varNodoNuevo;
            if (!EstaVacia())
            {
                if (atrLongitud - 1 == 0)
                {
                    prmItem = atrNodoPrimero.darItem();
                    atrNodoPrimero = null;
                    atrNodoUltimo = null;
                    atrLongitud--;
                    return true;
                }
                if (prmIndice == 0)
                {
                    prmItem = atrNodoPrimero.darItem();
                    varNodoNuevo = atrNodoPrimero.darSiguiente();
                    atrNodoPrimero = varNodoNuevo;
                    atrLongitud--;
                    return true;
                }
                if (IrIndice(prmIndice - 1))
                {
                    varNodoNuevo = atrNodoActual.darSiguiente();
                    prmItem = varNodoNuevo.darItem();
                    if (prmIndice == atrLongitud - 1)
                    {
                        atrNodoActual.ponerSiguiente(null);
                        atrNodoUltimo = atrNodoActual;
                        atrLongitud--;
                        return true;
                    }
                    atrNodoActual.ponerSiguiente(varNodoNuevo.darSiguiente());
                    varNodoNuevo.ponerSiguiente(null);
                    atrLongitud--;
                    return true;
                }
            }
            return false;
        }
        #endregion
        #region Accesores
        public clsNodoSimpleEnlazado<Tipo> darNodoPrimero() { return atrNodoPrimero; }
        public clsNodoSimpleEnlazado<Tipo> darNodoUltimo() { return atrNodoUltimo; }
        #endregion
        #region Iterador
        clsNodoSimpleEnlazado<Tipo> atrNodoActual;
        protected override bool IrIndice(int prmIndice)
        {
            if (!EstaVacia())
            {
                if (prmIndice == 0)
                {
                    atrIndiceActual = 0;
                    atrNodoActual = atrNodoPrimero;
                    atrItemActual = atrNodoActual.darItem();
                    return true;
                }
                if (prmIndice == atrLongitud - 1)
                {
                    atrIndiceActual = atrLongitud - 1;
                    atrNodoActual = atrNodoUltimo;
                    atrItemActual = atrNodoActual.darItem();
                    return true;
                }
                if (EsValido(prmIndice))
                {
                    atrNodoActual = atrNodoPrimero;
                    for (atrIndiceActual = 1; atrIndiceActual < prmIndice; atrIndiceActual++)
                        atrNodoActual = atrNodoActual.darSiguiente();
                    atrItemActual = atrNodoActual.darItem();
                    return true;
                }
            }
            return false;
        }
        protected override void PonerItemActual(){atrNodoActual.ponerItem(atrItemActual);}
        #endregion
        #endregion
    }
}
