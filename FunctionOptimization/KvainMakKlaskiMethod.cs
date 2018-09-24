using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionOptimization
{
    /// <summary>
    /// https://en.wikipedia.org/wiki/Quine–McCluskey_algorithm
    /// </summary>
    public static class PositiveMcCluskeyMethod
    {
        public static ImplicantDisjunctionNormalForm GetImplicantDisjunctionNormalForm(bool[] outputs)
        {
            List<Implicant> constituents = GetConstituents(outputs);
            TreeNode<IReadOnlyList<Implicant>> implicantTree = GetImplicantsTreeNode(constituents);
            List<Implicant> implicantTreeInlined = InlineTreeFromLeavesToRoot(implicantTree);
            List<Implicant> shortDisjunctionNormalFormImplicants = AbsorpImplicantsToShortDisjunctionNormalForm(implicantTreeInlined);
            List<Implicant> minimalDisjuncionNormalForm = GetMinimalDisjunctionNormalForm(
                constituents, shortDisjunctionNormalFormImplicants);
            return new ImplicantDisjunctionNormalForm(minimalDisjuncionNormalForm);
        }

        /// <summary>
        /// Petrick's method (https://en.wikipedia.org/wiki/Petrick%27s_method) is too complicated for lab work programming.
        /// Thats why lets implement primitive method based on teacher's explanation.
        /// </summary>
        private static List<Implicant> GetMinimalDisjunctionNormalForm(List<Implicant> constituents, List<Implicant> implicants)
        {
            var coreImplicants = new HashSet<Implicant>();
            foreach (Implicant implicant in implicants)
            {
                foreach (Implicant constituent in constituents)
                {
                    List<Implicant> implicantsInConstituent = implicants.Where(i => i.IsSubsetOf(constituent)).ToList();

                    if (implicantsInConstituent.Count == 1 && implicantsInConstituent.Contains(implicant))
                    {
                        coreImplicants.Add(implicant);
                        continue;
                    }
                }
            }
            List<Implicant> noncoreImplicants = implicants.Except(coreImplicants).ToList();

            List<Implicant> notCoveredConstituents = constituents
                .Where(c => !coreImplicants.Any(coreImplicant => coreImplicant.IsSubsetOf(c))).ToList();

            List<Implicant> resultImplicants = coreImplicants.ToList();
            
            while (notCoveredConstituents.Count > 0)
            {
                noncoreImplicants = noncoreImplicants
                    .OrderBy(i => notCoveredConstituents.Where(c => i.IsSubsetOf(c)).Count()).ToList();
                Implicant noncoreTopRatedImplicant = noncoreImplicants[noncoreImplicants.Count - 1];
                noncoreImplicants.RemoveAt(noncoreImplicants.Count - 1);

                bool covers = false;
                for (int i = notCoveredConstituents.Count - 1; i >= 0; i--)
                {
                    if (noncoreTopRatedImplicant.IsSubsetOf(notCoveredConstituents[i]))
                    {
                        notCoveredConstituents.RemoveAt(i);
                        covers = true;
                    }
                }
                if (covers)
                {
                    resultImplicants.Add(noncoreTopRatedImplicant);
                }
            }

            return resultImplicants;
        }

        /// <param name="implicants">Implicants ordered from short to long.</param>
        private static List<Implicant> AbsorpImplicantsToShortDisjunctionNormalForm(List<Implicant> implicants)
        {
            List<Implicant> resultList = implicants.ToList();

            for (int absorperIndex = 0; absorperIndex < resultList.Count - 1; absorperIndex++)
            {
                Implicant absorper = resultList[absorperIndex];
                for (int absorpedIndex = resultList.Count - 1; absorpedIndex > absorperIndex; absorpedIndex--)
                {
                    Implicant absorped = resultList[absorpedIndex];
                    if (absorper.IsSubsetOf(absorped))
                    {
                        resultList.RemoveAt(absorpedIndex);
                    }
                }
            }

            return resultList;
        }

        private static List<Implicant> InlineTreeFromLeavesToRoot(TreeNode<IReadOnlyList<Implicant>> rootNode)
        {
            var resultList = new List<Implicant>();

            void Visit(TreeNode<IReadOnlyList<Implicant>> node)
            {
                foreach (TreeNode<IReadOnlyList<Implicant>> child in node.Children)
                {
                    Visit(child);
                }
                resultList.AddRange(node.Value);
            }

            Visit(rootNode);

            return resultList;
        }

        private static TreeNode<IReadOnlyList<Implicant>> GetImplicantsTreeNode(
            IReadOnlyList<Implicant> currentLayerImplicants)
        {
            IReadOnlyList<IReadOnlyList<Implicant>> implicantsGroupedByPositiveBitsCount = GroupByPositiveBitsCount(currentLayerImplicants);

            IReadOnlyList<Tuple<Implicant, Implicant>> constituentPairs = GetConstituentPairsFromGroups(implicantsGroupedByPositiveBitsCount);
            IReadOnlyList<IReadOnlyList<Implicant>> nextLayerGroups = GetImplicantsGroupedByDifferencePosition(constituentPairs);

            var childrenNodes = new List<TreeNode<IReadOnlyList<Implicant>>>();
            foreach (IReadOnlyList<Implicant> nextLayerGroup in nextLayerGroups)
            {
                TreeNode<IReadOnlyList<Implicant>> childNode = GetImplicantsTreeNode(nextLayerGroup);
                childrenNodes.Add(childNode);
            }
            return new TreeNode<IReadOnlyList<Implicant>>(currentLayerImplicants, childrenNodes);
        }

        private static List<Implicant> InlineGroups(List<List<Implicant>> groups)
        {
            var resultLayer = new List<Implicant>();
            foreach (List<Implicant> group in groups)
            {
                resultLayer.AddRange(group);
            }
            return resultLayer;
        }

        private static IReadOnlyList<IReadOnlyList<Implicant>> GetImplicantsGroupedByDifferencePosition(
            IEnumerable<Tuple<Implicant, Implicant>> constituentPairs)
        {
            var implicantsGroupped = new SortedDictionary<int, List<Implicant>>();
            foreach (Tuple<Implicant, Implicant> item in constituentPairs)
            {
                Implicant commonImplicant;
                int signDifferenceIndex;
                if (Implicant.TestDifferenceInOneSign(item.Item1, item.Item2, out commonImplicant, out signDifferenceIndex))
                {
                    List<Implicant> group;
                    if (!implicantsGroupped.TryGetValue(signDifferenceIndex, out group))
                    {
                        group = new List<Implicant>();
                        implicantsGroupped[signDifferenceIndex] = group;
                    }
                    group.Add(commonImplicant);
                }
            }
            return implicantsGroupped.Values.ToList();
        }



        private static IReadOnlyList<Tuple<Implicant, Implicant>> GetConstituentPairsFromGroups(
            IReadOnlyList<IReadOnlyList<Implicant>> groups)
        {
            var pairsList = new List<Tuple<Implicant, Implicant>>();
            for (int groupFromIndex = 0; groupFromIndex < groups.Count - 1; groupFromIndex++)
            {
                IReadOnlyList<Implicant> groupFrom = groups[groupFromIndex];
                for (int groupToIndex = groupFromIndex + 1; groupToIndex < groups.Count; groupToIndex++)
                {
                    IReadOnlyList<Implicant> groupTo = groups[groupToIndex];

                    foreach (var from in groupFrom)
                    {
                        foreach (var to in groupTo)
                        {
                            pairsList.Add(new Tuple<Implicant, Implicant>(from, to));
                        }
                    }
                }
            }
            return pairsList;
        }

        public static IReadOnlyList<IReadOnlyList<Implicant>> GroupByPositiveBitsCount(
            IEnumerable<Implicant> implicants)
        {
            var dictionary = new SortedDictionary<int, List<Implicant>>();
            foreach (Implicant item in implicants)
            {
                int positiveBitsCount = item.GetPositiveBitsCount();
                List<Implicant> group;
                if (!dictionary.TryGetValue(positiveBitsCount, out group))
                {
                    group = new List<Implicant>();
                    dictionary[positiveBitsCount] = group;
                }
                group.Add(item);
            }
            return dictionary.Values.ToList();
        }

        public static List<Implicant> GetConstituents(bool[] outputs)
        {
            List<uint> trueNumbers = PerfectDisjunctionNormalFormBinary.GetTrueNumbers(outputs);
            List<Implicant> constituents = UIntListToInputTupleList(trueNumbers);
            return constituents;
        }

        public static List<Implicant> UIntListToInputTupleList(List<uint> items)
        {
            var result = new List<Implicant>();
            foreach (uint n in items)
            {
                result.Add(
                    new Implicant(
                        new InputSign[]
                        {
                            new InputSign(0, !BitTools.GetBit(n, 0)),
                            new InputSign(1, !BitTools.GetBit(n, 1)),
                            new InputSign(2, !BitTools.GetBit(n, 2)),
                            new InputSign(3, !BitTools.GetBit(n, 3)),
                        }
                    )
                );
            }
            return result;
        }
    }
}
