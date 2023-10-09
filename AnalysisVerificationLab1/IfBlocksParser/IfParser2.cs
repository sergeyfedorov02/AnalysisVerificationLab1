using System.Collections.Generic;
using System.Linq;

namespace AnalysisVerificationLab1.IfBlocksParser
{
    public class IfParser2
    {
        // Find lasts blocks keys in this If-block
        public static List<int> FindLastIfBlockKeys(List<int> curRes, Dictionary<int, int> lastKeysDictionary,
            Dictionary<int, List<int>> connections, Dictionary<int, string> linesDictionary,
            KeyValuePair<int, List<int>> currentNode)
        {
            foreach (var value in currentNode.Value)
            {
                // lastKeysDictionary contains value
                if (lastKeysDictionary.ContainsKey(value))
                {
                    var newRes = lastKeysDictionary.SingleOrDefault(x => x.Key.Equals(value)).Key;
                    curRes.Add(newRes);
                    return curRes;
                }

                if (!connections.ContainsKey(value))
                {
                    var linesValue = linesDictionary[value];

                    // ignore Return-block
                    if (linesValue.Length > 7 && linesValue[..7].Equals("return "))
                    {
                    }
                    else
                    {
                        curRes.Add(value);
                    }
                }
                else
                {
                    var newNode = connections.SingleOrDefault(x => x.Key == value);
                    FindLastKeyBlock(curRes, lastKeysDictionary, connections, linesDictionary, newNode);
                }
            }

            return curRes;
        }

        public static List<int> FindLastKeyBlock(List<int> curRes, Dictionary<int, int> lastKeysDictionary,
            Dictionary<int, List<int>> connections, Dictionary<int, string> linesDictionary,
            KeyValuePair<int, List<int>> currentNode)
        {
            foreach (var value in currentNode.Value)
            {
                // lastKeysDictionary contains value
                if (lastKeysDictionary.ContainsKey(value))
                {
                    var newRes = lastKeysDictionary.SingleOrDefault(x => x.Key.Equals(value)).Key;
                    curRes.Add(newRes);
                    return curRes;
                }

                if (!connections.ContainsKey(value))
                {
                    var linesValue = linesDictionary[value];

                    // ignore Return-block
                    if (linesValue.Length > 7 && linesValue[..7].Equals("return "))
                    {
                    }
                    else
                    {
                        curRes.Add(value);
                    }
                }
                else
                {
                    var newNode = connections.SingleOrDefault(x => x.Key == value);
                    FindLastKeyBlock(curRes, lastKeysDictionary, connections, linesDictionary, newNode);
                }
            }

            return curRes;
        }

        // Find next block Key
        public static int FindNextBlockKey(Dictionary<int, List<int>> connections,
            Dictionary<int, string> linesDictionary, int ifBlockKey, Dictionary<int, List<int>> blocksDictionary,
            int lastValue, Dictionary<int, int> incrementBlocksDictionary, Dictionary<int, int> forDictionary)
        {
            var currentIfBlockConnections = connections[ifBlockKey];
            var resultValue = lastValue;

            // next+true+false
            if (currentIfBlockConnections.Count == 3)
            {
                return currentIfBlockConnections.SingleOrDefault(x =>
                    !linesDictionary[x].Equals("Block") && !linesDictionary[x].Equals("else"));
            }

            // next + true
            if (currentIfBlockConnections.Count == 2)
            {
                var notBlockConnection = blocksDictionary.ContainsKey(currentIfBlockConnections[0])
                    ? currentIfBlockConnections[1]
                    : currentIfBlockConnections[0];

                if (!linesDictionary[notBlockConnection].Equals("else"))
                {
                    return notBlockConnection;
                }
            }

            // next or next + false -> check Parents while don't find
            resultValue++;
            while (!linesDictionary.ContainsKey(resultValue))
            {
                resultValue++;
            }

            // check are we inside block-for
            var currentValueFor = TryFindParentBlockFor(connections, linesDictionary, incrementBlocksDictionary,
                forDictionary, lastValue);
            var lastValueFor = TryFindParentBlockFor(connections, linesDictionary, incrementBlocksDictionary,
                forDictionary, resultValue);

            /*if (currentValueFor != lastValueFor)
            {
                resultValue = incrementBlocksDictionary[currentValueFor];
            }*/


            return resultValue;
        }

        private static int TryFindParentBlockFor(Dictionary<int, List<int>> connections,
            Dictionary<int, string> linesDictionary, Dictionary<int, int> incrementBlocksDictionary,
            Dictionary<int, int> forDictionary, int testValue)
        {
            var result = 0;
            return result;
        }

        // Haven't got Else-block -> add connection from current to next
        // Have got Else-block -> delete connection from current to next
        public static void AddConnectionToNextFromCurrentIf(Dictionary<int, List<int>> connections,
            Dictionary<int, string> linesDictionary, int nextBlockKey, int ifBlockKey)
        {
            var currentIfBlockConnections = connections[ifBlockKey];
            var elseFlag = false;
            foreach (var value in currentIfBlockConnections)
            {
                var lineValue = linesDictionary[value];
                if (lineValue.Length == 4 && lineValue.Equals("else"))
                {
                    elseFlag = true;
                    //have got connection if -> next
                    if (connections[ifBlockKey].Contains(nextBlockKey))
                    {
                        // delete connection if -> next
                        connections.SingleOrDefault(x => x.Key == ifBlockKey).Value.Remove(nextBlockKey);
                    }

                    break;
                }
            }

            if (!elseFlag)
            {
                // haven't got connection if->next
                if (!connections[ifBlockKey].Contains(nextBlockKey))
                {
                    // add connection if->next
                    connections[ifBlockKey].Add(nextBlockKey);
                }
            }
        }

        // Create if-labels Dictionary
        public static Dictionary<KeyValuePair<int, int>, string> CreateIfLabelsDictionary(
            Dictionary<int, List<int>> connections, Dictionary<int, string> linesDictionary)
        {
            var result = new Dictionary<KeyValuePair<int, int>, string>();
            var ifDictionary = new Dictionary<int, List<int>>();

            foreach (var (key, value) in linesDictionary)
            {
                if (value.Length > 3 && value[..3].Equals("if "))
                {
                    ifDictionary[key] = connections[key];
                }
            }

            foreach (var (key, values) in ifDictionary)
            {
                // find block-then -> true
                var valueMin = values.Min();

                // find block-else -> false
                var valueMax = values.Max();

                var minPair = new KeyValuePair<int, int>(key, valueMin);
                var maxPair = new KeyValuePair<int, int>(key, valueMax);

                result[minPair] = "true";
                result[maxPair] = "false";
            }

            return result;
        }

        // Delete Block from If with letters "Block", "else"
        public static Dictionary<int, List<int>> DeleteIfBlocks(Dictionary<int, List<int>> connections,
            Dictionary<int, string> linesDictionary, Dictionary<KeyValuePair<int, int>, string> labelsDictionary)
        {
            var result = new Dictionary<int, List<int>>(connections);
            var ifDictionary = new Dictionary<int, List<int>>();

            foreach (var (key, value) in linesDictionary)
            {
                if (value.Length > 3 && value[..3].Equals("if "))
                {
                    ifDictionary[key] = connections[key];
                }
            }

            var valuesToDeleteThen = new Dictionary<int, int>();
            var valuesToAddThen = new Dictionary<int, int>();

            var valuesToDeleteElse = new List<KeyValuePair<int, int>>();
            var valuesToAddElse = new Dictionary<int, int>();

            foreach (var (key, values) in ifDictionary)
            {
                foreach (var value in values)
                {
                    var curValue = linesDictionary[value];
                    switch (curValue.Length)
                    {
                        case 5 when curValue.Equals("Block"):
                        {
                            var nextNode = result[value][0];
                            valuesToAddThen[key] = nextNode;
                            valuesToDeleteThen[key] = value;
                            break;
                        }
                        case 4 when curValue.Equals("else"):
                        {
                            var nextBlock = result[value][0];
                            var first = new KeyValuePair<int, int>(key, value);

                            var nextNode = result[nextBlock][0];
                            valuesToAddElse[key] = nextNode;
                            var second = new KeyValuePair<int, int>(value, nextBlock);

                            valuesToDeleteElse.Add(first);
                            valuesToDeleteElse.Add(second);
                            break;
                        }
                    }
                }
            }

            foreach (var (key, value) in valuesToAddThen)
            {
                result[key].Add(value);
            }

            foreach (var (key, value) in valuesToDeleteThen)
            {
                var curPair = new KeyValuePair<int, int>(key, value);
                var tryGetValuePair = labelsDictionary.TryGetValue(curPair, out string stringValue);
                if (tryGetValuePair)
                {
                    var newValue = valuesToAddThen.SingleOrDefault(x => x.Key == key);
                    labelsDictionary.Remove(curPair);
                    labelsDictionary[newValue] = stringValue;
                }

                result.Remove(value);
                result.SingleOrDefault(x => x.Key == key).Value.Remove(value);
                linesDictionary.Remove(value);
            }

            foreach (var (key, value) in valuesToAddElse)
            {
                result[key].Add(value);
            }

            for (var i = 0; i < valuesToDeleteElse.Count; i += 2)
            {
                var curValueFirst = valuesToDeleteElse[i];
                var curValueSecond = valuesToDeleteElse[i + 1];

                var curPair = new KeyValuePair<int, int>(curValueFirst.Key, curValueFirst.Value);
                var tryGetValuePair = labelsDictionary.TryGetValue(curPair, out string stringValue);
                if (tryGetValuePair)
                {
                    var newValue = valuesToAddElse.SingleOrDefault(x => x.Key == curValueFirst.Key);
                    labelsDictionary.Remove(curPair);
                    labelsDictionary[newValue] = stringValue;
                }

                result.Remove(curValueFirst.Value);
                result.SingleOrDefault(x => x.Key == curValueFirst.Key).Value.Remove(curValueFirst.Value);
                linesDictionary.Remove(curValueFirst.Value);

                result.Remove(curValueSecond.Value);
                linesDictionary.Remove(curValueSecond.Value);
            }

            return result;
        }
    }
}