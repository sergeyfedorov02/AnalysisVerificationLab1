using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AnalysisVerificationLab1.IfBlocksParser;
using AnalysisVerificationLab1.NodeParsers;
using AnalysisVerificationLab1.NodeParsers.TypesParsers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AnalysisVerificationLab1
{
    public class PreviousProgram
    {
        private static readonly Dictionary<int, List<int>> NodeDictionary = new();
        private static readonly Dictionary<int, string> LinesDictionary = new();
        
        static void Mains(string[] args)
        {
            var testsCount = 10;
            for (var i = 1; i <= testsCount; i++)
            {
                
            }
            
            string code = @"class MyClass
                        {
                            private static int Main(int x, String y)
                            {
                                var c = 0;
                                int a = 5;      
                                int b = 7;
                                String xx;
                                if (a >= b)
                                {
                                    x = i;
                                    c = a - b;
                                }
                                else 
                                {
                                    c = b - a;
                                    x = else;
                                }
                                var result = c;
                                return result;    
                            }
                        }";
            
            var programCode =
                File.ReadAllText("D:\\RiderProjects\\AnalysisVerificationLab1\\AnalysisVerificationLab1\\TestsClasses\\ClassTest.cs");

            
            SyntaxTree tree = CSharpSyntaxTree.ParseText(programCode);
            SyntaxNode root = tree.GetRoot();

            var methodNodes = from methodDeclaration in root.DescendantNodes()
                    .Where(x => x is MethodDeclarationSyntax)
                select methodDeclaration;

            var filePath = "D:\\RiderProjects\\AnalysisVerificationLab1\\AnalysisVerificationLab1\\Program.dot";

            GenerateDotFile(root, filePath);
        }
        
        private static void GenerateDotFile(SyntaxNode node, string filePath)
        {
            using StreamWriter file = new(filePath, append: false);
            file.WriteLine("digraph G {");

            int nodeId = 0;
            SerializeGraphNode(node, ref nodeId, file);
            
            // TODO
            var newAlgorithm = CreateNewConnectionsNodes(NodeDictionary, LinesDictionary);

            file.WriteLine("}");
        }
        
        private static void SerializeGraphNode(SyntaxNode node, ref int nodeId, StreamWriter file)
        {
            string newLine = null;
            var nodeIdCurrent = nodeId;
            var nodeKind = node.Kind();

            // TODO - DELETE at the end!!!
            List<String> nodeTypes = new List<string>();

            // получает список всех узлов, находящихся в дереве ниже текущего
            var descendantTokens = node.DescendantTokens();
            var syntaxKindOfNode = node.GetType().Name;

            // Проход по типам узлов для разбора всех случаев 
            switch (node)
            {
                // class
                case ClassDeclarationSyntax classDeclaration:
                {
                    var classDeclarationInstance = new ClassDeclaration();
                    newLine = classDeclarationInstance.GetClassDeclarationParserResult(classDeclaration);
                    break;
                }

                //method
                case MethodDeclarationSyntax methodDeclaration:
                {
                    var methodDeclarationInstance = new MethodDeclaration();
                    newLine = methodDeclarationInstance.GetMethodDeclarationParserResult(methodDeclaration);
                    break;
                }

                //arguments in Method
                case ParameterListSyntax parameterList:
                {
                    var parameterListInstance = new ParameterList();
                    newLine = parameterListInstance.GetParameterListParserResult(parameterList);
                    break;
                }

                //assigment (a = b) or (new List<string> or new HashMap<int, string>)
                case LocalDeclarationStatementSyntax localDeclarationStatement:
                {
                    var localDeclarationStatementInstance = new LocalDeclarationStatement();
                    newLine =
                        localDeclarationStatementInstance.GetLocalDeclarationStatementParserResult(
                            localDeclarationStatement);
                    break;
                }

                // operator (+,+=, -, *, ++, ...)
                case ExpressionStatementSyntax expressionStatement:
                {
                    var expressionStatementInstance = new ExpressionStatement();
                    newLine = expressionStatementInstance.GetExpressionStatementParserResult(expressionStatement);
                    break;
                }

                // if ( a>=b) or (5 < 4)
                // work only with BinaryOperations: ==, >=, <=, !=
                case IfStatementSyntax ifStatement:
                {
                    var ifStatementInstance = new IfStatement();
                    newLine = ifStatementInstance.GetIfStatementParserResult(ifStatement);
                    break;
                }

                case BlockSyntax:
                {
                    newLine = "Block";
                    break;
                }

                case ElseClauseSyntax:
                {
                    newLine = "else";
                    break;
                }

                // return
                case ReturnStatementSyntax returnStatement:
                {
                    var returnStatementInstance = new ReturnStatement();
                    newLine = returnStatementInstance.GetReturnStatementParserResult(returnStatement);
                    break;
                }

                // for (int or var i = 0; i < b; i++)
                // work only with int values (int b; b.Count - int)
                case ForStatementSyntax forStatementSyntax:
                    var forStatementInstance = new ForStatement();
                    newLine = forStatementInstance.GetForStatementParserResult(forStatementSyntax);
                    break;

                // TODO
                // while (x > 5) or (5 < x) or (x != y)
                // work only with int values (int x; x.Count - int)
                case WhileStatementSyntax whileStatementSyntax:
                    var whileStatementInstance = new WhileStatement();
                    newLine = whileStatementInstance.GetWhileStatementParserResult(whileStatementSyntax);
                    break;


                // TODO
                // break
                case BreakStatementSyntax breakStatementSyntax:
                    var breakStatementInstance = new BreakStatement();
                    newLine = breakStatementInstance.GetBreakStatementParserResult(breakStatementSyntax);
                    break;

                // TODO - optional
                // continue

                // unknowns syntax: switch, case, continue, do-while...
                default:
                {
                    // TODO - DELETE at the end!!!
                    nodeTypes.Add(node.GetType().Name);
                    break;
                }
            }

            if (newLine != null)
            {
                LinesDictionary.Add(nodeIdCurrent, newLine);
            }

            file.WriteLine($" node{nodeIdCurrent}[shape=box, label=\"{newLine}\"]");
            //file.WriteLine($" node{nodeIdCurrent}[label=\"{nodeKind}\"]");

            foreach (var child in node.ChildNodes())
            {
                nodeId++;
                var nodeIdChild = nodeId;

                if (NodeDictionary.ContainsKey(nodeIdCurrent))
                {
                    NodeDictionary[nodeIdCurrent].Add(nodeIdChild);
                }
                else
                {
                    var values = new List<int> { nodeIdChild };
                    NodeDictionary[nodeIdCurrent] = values;
                }

                file.WriteLine($" node{nodeIdCurrent} -> node{nodeIdChild}");
                SerializeGraphNode(child, ref nodeId, file);
            }
        }

        // TODO
        private static Dictionary<int, List<int>> CreateNewConnectionsNodes(Dictionary<int, List<int>> nodeDictionary,
            Dictionary<int, string> linesDictionary)
        {
            var result = new Dictionary<int, List<int>>();
            var newLinesDictionary = new Dictionary<int, string>(linesDictionary);

            var filePath = "D:\\RiderProjects\\AnalysisVerificationLab1\\AnalysisVerificationLab1\\Program2.dot";
            using StreamWriter file = new(filePath, append: false);
            file.WriteLine("digraph F {");

            // Create Connections Before First Block
            foreach (var (key, value) in newLinesDictionary)
            {
                // check first Block -> break
                if (value.Equals("Block"))
                {
                    break;
                }

                // it's not the first Block
                var currentIndex = Array.IndexOf(newLinesDictionary.Keys.ToArray(), key);
                if (currentIndex == -1 || currentIndex + 1 >= newLinesDictionary.Keys.Count) continue;

                var nextKey = newLinesDictionary.Keys.ToArray()[currentIndex + 1];
                newLinesDictionary.Remove(key);
                result[key] = new List<int> { nextKey };
                file.WriteLine($" node{key}[label=\"{value}\"]");
            }

            var ifDictionary = CreateIfDictionary(nodeDictionary, newLinesDictionary);
            var elseDictionary = CreateElseDictionary(nodeDictionary, newLinesDictionary);
            var blockDictionary = CreateBlockDictionary(nodeDictionary, newLinesDictionary);
            var forDictionary = CreateForDictionary(nodeDictionary, newLinesDictionary);
            var ifElsePairs = CreateIfElsePair(newLinesDictionary);

            var firstBlockKey = blockDictionary.First().Key;

            // CreateBlocksConnections
            foreach (var (key, value) in blockDictionary)
            {
                var currentIndex = 0;
                result[key] = new List<int> { value[currentIndex] };
                foreach (var v in value.Where(_ => value.Count - 1 - currentIndex > 0))
                {
                    result[v] = new List<int> { value[currentIndex + 1] };
                    currentIndex++;
                }
            }

            // CreateElseConnections
            foreach (var (key, value) in elseDictionary)
            {
                result[key] = new List<int> { value };
            }

            // CreateIfConnections
            foreach (var (key, value) in ifDictionary)
            {
                foreach (var v in value)
                {
                    if (result.Keys.Contains(key))
                    {
                        result[key].Add(v);
                    }
                    else
                    {
                        result[key] = new List<int> { v };
                    }
                }
            }

            // Create ForConnections
            foreach (var (key, value) in forDictionary)
            {
                result[key] = new List<int> { value };
            }


            // Create For-Parser
            // create Declaration-block before For-block + create Increment-block
            var incrementBlocksDictionary = new Dictionary<int, int>();
            foreach (var (key, value) in forDictionary)
            {
                var declarationBlockId = value - 1;
                var incrementBlockId = value - 2;
                var splitValue = newLinesDictionary[key].Split(";");

                var declarationValue = splitValue[1];
                var incrementValue = splitValue[3];

                // create record
                newLinesDictionary[declarationBlockId] = declarationValue;
                newLinesDictionary[incrementBlockId] = incrementValue;

                newLinesDictionary[key] = $"{splitValue[0]};{splitValue[2]}";

                //change value in result
                var preNodeKey = result.SingleOrDefault(x => x.Value.Contains(key)).Key;
                var values = result[preNodeKey];
                values[values.IndexOf(key)] = declarationBlockId;

                //create new connection in result
                result[declarationBlockId] = new List<int> { key };
                result[incrementBlockId] = new List<int> { key };

                incrementBlocksDictionary[key] = incrementBlockId;
            }

            // Create else-branch(next actions) to for-blocks
            var emptyNodes = new List<int>();
            foreach (var (key, _) in newLinesDictionary)
            {
                if (!incrementBlocksDictionary.Values.Contains(key))
                {
                    var curBool = true;
                    foreach (var (_, values) in result)
                    {
                        if (values.Contains(key))
                        {
                            curBool = false;
                        }
                    }

                    if (curBool)
                    {
                        emptyNodes.Add(key);
                    }
                }
            }

            // create connections after for-block: for-block -> nextBlock
            var reverseKeysBlockDictionary = blockDictionary.Keys.Reverse().ToList();
            foreach (var (key, _) in forDictionary)
            {
                foreach (var blockId in reverseKeysBlockDictionary)
                {
                    if (blockId < key)
                    {
                        var breakFlag = false;
                        var curBlockChildren = blockDictionary[blockId];
                        foreach (var blockChild in curBlockChildren)
                        {
                            if (emptyNodes.Contains(blockChild))
                            {
                                result[key].Add(blockChild);
                                breakFlag = true;
                                break;
                            }
                        }

                        if (breakFlag)
                        {
                            break;
                        }
                    }
                }
            }


            // Create If-Parser
            //Find IF in graph and create type for his children
            var ifTypesDictionary = IfParser.CreateIfChildrenType(result, newLinesDictionary);

            // Create new connections with if-then-else
            var resultAfterIf =
                IfParser.CreateNewConnectionsWithIfTypes(result, newLinesDictionary, ifTypesDictionary);
            result = resultAfterIf;

            // Create if-labels
            var ifLabelsDictionary = IfParser.CreateIfLabelsDictionary(result, newLinesDictionary);

            // Delete Block from If with letters "Block", "else"
            var resultAfterDelIfBlocks = IfParser.DeleteIfBlocks(result, newLinesDictionary, ifLabelsDictionary);
            result = resultAfterDelIfBlocks;


            // Create For-Parser - continue
            // Create connections to Increment after iteration is end in for-block 
            var lastKeysBlockForDictionary = new Dictionary<int, int>();
            var reverseValuesFromIncrementBlocksDictionary = incrementBlocksDictionary.Values.Reverse().ToList();
            foreach (var increment in reverseValuesFromIncrementBlocksDictionary)
            {
                var keyBlockFor = incrementBlocksDictionary.SingleOrDefault(x => x.Value.Equals(increment)).Key;
                var blockForBlockKey = forDictionary.SingleOrDefault(x => x.Key.Equals(keyBlockFor)).Value;
                var currentNode = result.SingleOrDefault(x => x.Key.Equals(blockForBlockKey));
                var currentLastKeys =
                    FindLastKeyBlockFor(new List<int>(), lastKeysBlockForDictionary, result, newLinesDictionary,
                        currentNode).Distinct().ToList();

                var currentLastKeysWithoutValue = currentLastKeys
                    .Where(value => !lastKeysBlockForDictionary.ContainsValue(value)).ToList();
                var currentLastKey = currentLastKeys[0];
                if (currentLastKeysWithoutValue.Count != 1)
                {
                    currentLastKey =
                        currentLastKeysWithoutValue.SingleOrDefault(x => !lastKeysBlockForDictionary.ContainsKey(x));
                }

                if (!result.ContainsKey(currentLastKey))
                {
                    result[currentLastKey] = new List<int> { increment };
                }

                else
                {
                    result[currentLastKey].Add(increment);
                }

                lastKeysBlockForDictionary[keyBlockFor] = currentLastKey;
            }

            // Delete false-connections with not-increment block
            var resultAfterDeleteForConnectionsFalseBranch =
                DeleteForConnectionsFalseBranch(result, newLinesDictionary, incrementBlocksDictionary.Values.ToList());
            result = resultAfterDeleteForConnectionsFalseBranch;

            // Create for-labels
            var forLabelsDictionary = CreateForLabelsDictionary(result, newLinesDictionary);

            // Delete Block from For with letters "Block"
            var resultAfterDelForBlocks = DeleteForBlocks(result, newLinesDictionary, forLabelsDictionary);
            result = resultAfterDelForBlocks;

            // TODO - фиксануть баг из примера Test7 -> Test2-норм!!!
            // TODO - move For_parser logic to ForBlocksParser (create Classes before-if + after-if && remove ForParser class)
            var xx = 0;


            //TODO
            // Create While-Parser

            //TODO
            // Create Break-Parser

            //TODO - optional
            // Create Continue-Parser

            //TODO
            // Merged returns Blocks to 1 final exit-block

            // Delete first Block
            var resultAfterDelFirstBlock = DeleteFirstBlock(result, newLinesDictionary, firstBlockKey);
            result = resultAfterDelFirstBlock;

            // Create merged labels dictionary
            var labelsDictionary = ifLabelsDictionary.Union(forLabelsDictionary)
                .ToDictionary(x => x.Key, x => x.Value);

            // Create DOT file
            PrintNodes(result, newLinesDictionary, file, labelsDictionary);

            file.WriteLine("}");
            return result;
        }
        
        private static Dictionary<int, List<int>> DeleteForConnectionsFalseBranch(
            Dictionary<int, List<int>> connections, Dictionary<int, string> linesDictionary,
            List<int> incrementBlocksIdList)
        {
            var result = new Dictionary<int, List<int>>(connections);
            var forDictionary = new Dictionary<int, List<int>>();
            foreach (var (key, value) in linesDictionary)
            {
                if (!(value.Length > 3 & value[..3].Equals("for"))) continue;
                forDictionary[key] = connections[key];
            }

            var keyValueToDel = new Dictionary<int, int>();

            foreach (var (key, values) in forDictionary)
            {
                if (values.Count == 2) continue;
                foreach (var value in values.Where(value => !incrementBlocksIdList.Contains(value)))
                {
                    if (!linesDictionary[value].Equals("Block"))
                    {
                        keyValueToDel[key] = value;
                    }
                }
            }

            foreach (var (key, value) in keyValueToDel)
            {
                result.SingleOrDefault(x => x.Key == key).Value.Remove(value);
            }

            return result;
        }

        private static List<int> FindLastKeyBlockFor(List<int> curRes, Dictionary<int, int> lastKeysDictionary,
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
                    FindLastKeyBlockFor(curRes, lastKeysDictionary, connections, linesDictionary, newNode);
                }
            }

            return curRes;
        }


        private static Dictionary<int, List<int>> DeleteForBlocks(Dictionary<int, List<int>> connections,
            Dictionary<int, string> linesDictionary, Dictionary<KeyValuePair<int, int>, string> labelsDictionary)
        {
            var result = new Dictionary<int, List<int>>(connections);
            var forDictionary = new Dictionary<int, List<int>>();
            foreach (var (key, value) in linesDictionary)
            {
                if (!(value.Length > 3 & value[..3].Equals("for"))) continue;
                forDictionary[key] = connections[key];
            }

            var valuesToDeleteTrue = new Dictionary<int, int>();
            var valuesToAddTrue = new Dictionary<int, int>();

            foreach (var (key, values) in forDictionary)
            {
                foreach (var value in values)
                {
                    var curValue = linesDictionary[value];
                    if (!(curValue.Length == 5 & curValue.Equals("Block"))) continue;

                    var nextNode = result[value][0];
                    valuesToAddTrue[key] = nextNode;
                    valuesToDeleteTrue[key] = value;
                }
            }

            foreach (var (key, value) in valuesToAddTrue)
            {
                result[key].Add(value);
            }

            foreach (var (key, value) in valuesToDeleteTrue)
            {
                var curPair = new KeyValuePair<int, int>(key, value);
                var tryGetValuePair = labelsDictionary.TryGetValue(curPair, out string stringValue);
                if (tryGetValuePair)
                {
                    var newValue = valuesToAddTrue.SingleOrDefault(x => x.Key == key);
                    labelsDictionary.Remove(curPair);
                    labelsDictionary[newValue] = stringValue;
                }

                result.Remove(value);
                result.SingleOrDefault(x => x.Key == key).Value.Remove(value);
                linesDictionary.Remove(value);
            }

            return result;
        }

        private static Dictionary<KeyValuePair<int, int>, string> CreateForLabelsDictionary(
            Dictionary<int, List<int>> connections, Dictionary<int, string> linesDictionary)
        {
            var result = new Dictionary<KeyValuePair<int, int>, string>();
            var forDictionary = new Dictionary<int, List<int>>();
            foreach (var (key, value) in linesDictionary)
            {
                if (!(value.Length > 3 & value[..3].Equals("for"))) continue;
                forDictionary[key] = connections[key];
            }

            foreach (var (key, values) in forDictionary)
            {
                foreach (var value in values)
                {
                    var curPair = new KeyValuePair<int, int>(key, value);
                    if (linesDictionary[value].Equals("Block"))
                    {
                        result[curPair] = "true";
                    }
                    else
                    {
                        result[curPair] = "false";
                    }
                }
            }

            return result;
        }
        
        private static int FindNextBlockUp2(Dictionary<int, List<int>> connections,
            Dictionary<int, string> linesDictionary, KeyValuePair<int, List<int>> currentNode,
            Dictionary<int, List<int>> blockDictionary, Dictionary<int, int> incrementBlocksDictionary,
            Dictionary<int, List<int>> elseBranchLast)
        {
            var reverseKeysBlockDictionary = blockDictionary.Keys.Reverse().ToList();
            reverseKeysBlockDictionary.RemoveAt(reverseKeysBlockDictionary.Count - 1);

            var curNodeBlockIndex = currentNode.Value.FindIndex(x => linesDictionary[x].Equals("Block"));
            var currentNodeBlockKey = currentNode.Value[curNodeBlockIndex];

            foreach (var preBlockNodeKey in from key in reverseKeysBlockDictionary
                     where key < currentNodeBlockKey
                     select connections.SingleOrDefault(x => x.Value.Contains(key)).Key)
            {
                if (linesDictionary[preBlockNodeKey].Length > 4 && linesDictionary[preBlockNodeKey][..4].Equals("for;"))
                {
                    return incrementBlocksDictionary[preBlockNodeKey];
                }

                if (linesDictionary[preBlockNodeKey].Length > 6 &&
                    linesDictionary[preBlockNodeKey][..6].Equals("while;"))
                {
                    return preBlockNodeKey;
                }

                if (linesDictionary[preBlockNodeKey].Length > 3 &&
                    linesDictionary[preBlockNodeKey][..3].Equals("if "))
                {
                    var blockIfChildren = connections[preBlockNodeKey];
                    if (blockIfChildren.Count == 2)
                    {
                        // find next -> add connection
                        foreach (var value in blockIfChildren)
                        {
                            if (!linesDictionary[value].Equals("Block") && !linesDictionary[value].Equals("else"))
                            {
                                return value;
                            }
                        }

                        // find else -> try get next in else-branch or go up
                        var elseBranchKey = linesDictionary[blockIfChildren[0]].Equals("else")
                            ? blockIfChildren[0]
                            : blockIfChildren[1];
                        if (elseBranchLast.ContainsKey(preBlockNodeKey))
                        {
                            if (connections.ContainsKey(elseBranchLast[preBlockNodeKey][0]))
                            {
                                return connections[elseBranchLast[preBlockNodeKey][0]][0];
                            }
                        }
                    }
                }
            }

            return 0;
        }
        
        private static List<int> FindLastBlocksKeys(List<int> curRes, Dictionary<int, List<int>> connections,
            Dictionary<int, string> linesDictionary, KeyValuePair<int, List<int>> currentNode)
        {
            // find block-For or block-While
            if (linesDictionary[currentNode.Key].Length > 4 && linesDictionary[currentNode.Key][..4].Equals("for;") ||
                linesDictionary[currentNode.Key].Length > 6 && linesDictionary[currentNode.Key][..6].Equals("while;"))
            {
                var blockForWhile = connections.SingleOrDefault(x => x.Key.Equals(currentNode.Key));
                if (blockForWhile.Value.Count > 1)
                {
                    var nextBlockInForKey = linesDictionary[blockForWhile.Value[0]].Equals("Block")
                        ? blockForWhile.Value[1]
                        : blockForWhile.Value[0];
                    if (!connections.ContainsKey(nextBlockInForKey))
                    {
                        // TODO - del old version
                        /*if (linesDictionary[nextBlockInForKey].Length > 7 &&
                            linesDictionary[nextBlockInForKey][..7].Equals("return "))
                        {
                        }
                        else
                        {
                            curRes.Add(nextBlockInForKey);

                        }*/
                        switch (linesDictionary[nextBlockInForKey].Length)
                        {
                            // ignore Return-block
                            case > 7 when
                                linesDictionary[nextBlockInForKey][..7].Equals("return "):
                                break;
                            // ignore Break-block
                            case 6 when
                                linesDictionary[nextBlockInForKey].Equals("break;"):
                                break;
                            default:
                                curRes.Add(nextBlockInForKey);
                                break;
                        }
                    }
                    else
                    {
                        var newNode = connections.SingleOrDefault(x => x.Key == nextBlockInForKey);
                        FindLastBlocksKeys(curRes, connections, linesDictionary, newNode);
                    }
                }
                else
                {
                    curRes.Add(blockForWhile.Key);
                }
            }

            var isForBlock = linesDictionary[currentNode.Key].Length > 4 &&
                             linesDictionary[currentNode.Key][..4].Equals("for;");

            var iWhileBlock = linesDictionary[currentNode.Key].Length > 6 &&
                              linesDictionary[currentNode.Key][..6].Equals("while;");

            if (!isForBlock && !iWhileBlock)
            {
                foreach (var value in currentNode.Value)
                {
                    if (!connections.ContainsKey(value))
                    {
                        var linesValue = linesDictionary[value];

                        // TODO - del old version
                        /*// ignore Return-block
                        if (linesValue.Length > 7 && linesValue[..7].Equals("return "))
                        {
                        }
                        else
                        {
                            curRes.Add(value);
                        }*/

                        switch (linesValue.Length)
                        {
                            // ignore Return-block
                            case > 7 when linesValue[..7].Equals("return "):
                                break;
                            // ignore Break-block
                            case 6 when linesValue.Equals("break;"):
                                break;
                            default:
                                curRes.Add(value);
                                break;
                        }
                    }
                    else
                    {
                        var newNode = connections.SingleOrDefault(x => x.Key == value);
                        FindLastBlocksKeys(curRes, connections, linesDictionary, newNode);
                    }
                }
            }

            return curRes;
        }

        private static Dictionary<int, List<int>> DeleteFirstBlock(Dictionary<int, List<int>> connections,
            Dictionary<int, string> linesDictionary, int firstBlockKey)
        {
            var result = new Dictionary<int, List<int>>(connections);
            //var firstBlockKey = linesDictionary.First().Key;
            var preNodeKey = result.SingleOrDefault(x => x.Value.Contains(firstBlockKey)).Key;
            var nextNodeKey = result.SingleOrDefault(x => x.Key == firstBlockKey).Value[0];

            result[preNodeKey].Add(nextNodeKey);

            result.Remove(firstBlockKey);
            result.SingleOrDefault(x => x.Key == preNodeKey).Value.Remove(firstBlockKey);
            linesDictionary.Remove(firstBlockKey);

            return result;
        }

        private static void PrintNodes(Dictionary<int, List<int>> connections, Dictionary<int, string> names,
            StreamWriter file, Dictionary<KeyValuePair<int, int>, string> labelsDictionary)
        {
            // print Connections and Labels
            foreach (var (key, values) in connections)
            {
                foreach (var value in values)
                {
                    var newLine = $" node{key} -> node{value}";
                    var currentPair = new KeyValuePair<int, int>(key, value);
                    if (labelsDictionary.ContainsKey(currentPair))
                    {
                        var currentLabel = labelsDictionary[currentPair];
                        newLine = $" node{key} -> node{value}[label={currentLabel}]";
                    }

                    file.WriteLine(newLine);
                }
            }

            //print Names
            foreach (var (key, value) in names)
            {
                var currentValue = value;
                var currentShape = "box";
                if (value.Length > 3 && value[..3].Equals("if "))
                {
                    currentValue = value[3..];
                    currentShape = "diamond";
                }

                if (value.Length > 7 && value[..7].Equals("return "))
                {
                    currentShape = "ellipse";
                }

                file.WriteLine($" node{key}[shape={currentShape}, label=\"{currentValue}\"]");
            }
        }
        
        private static Dictionary<int, int> CreateForDictionary(Dictionary<int, List<int>> nodeDictionary,
            Dictionary<int, string> linesDictionary)
        {
            var result = new Dictionary<int, int>();
            foreach (var (key, value) in linesDictionary)
            {
                if (!(value.Length > 3 & value[..3].Equals("for"))) continue;

                var nodeConnections = nodeDictionary[key];
                foreach (var v in nodeConnections.Where(linesDictionary.ContainsKey))
                {
                    result[key] = v;
                    break;
                }
            }

            return result;
        }

        private static Dictionary<int, int> CreateElseDictionary(Dictionary<int, List<int>> nodeDictionary,
            Dictionary<int, string> linesDictionary)
        {
            var result = new Dictionary<int, int>();
            foreach (var (key, value) in linesDictionary)
            {
                if (value.Length == 4 & value.Equals("else"))
                {
                    result[key] = nodeDictionary[key][0];
                }
            }

            return result;
        }

        private static Dictionary<int, List<int>> CreateIfDictionary(Dictionary<int, List<int>> nodeDictionary,
            Dictionary<int, string> linesDictionary)
        {
            var result = new Dictionary<int, List<int>>();
            foreach (var (key, value) in linesDictionary)
            {
                if (!(value.Length > 2 & value[..3].Equals("if "))) continue;
                result[key] = new List<int>();
                foreach (var valueNode in nodeDictionary[key].Where(linesDictionary.ContainsKey))
                {
                    result[key].Add(valueNode);
                }
            }

            return result;
        }

        private static Dictionary<int, List<int>> CreateBlockDictionary(Dictionary<int, List<int>> nodeDictionary,
            Dictionary<int, string> linesDictionary)
        {
            var result = new Dictionary<int, List<int>>();
            foreach (var (key, value) in linesDictionary)
            {
                if (value.Length == 5 & value.Equals("Block"))
                {
                    result[key] = nodeDictionary[key];
                }
            }

            return result;
        }
        
        private static Dictionary<int, int> CreateIfElsePair(Dictionary<int, string> linesDictionary)
        {
            var result = new Dictionary<int, int>();

            var ifElseDictionary = new Dictionary<int, string>();
            foreach (var (key, value) in linesDictionary)
            {
                if (linesDictionary[key].Length > 2 & linesDictionary[key][..3].Equals("if "))
                {
                    ifElseDictionary[key] = "if";
                }
                else if (linesDictionary[key].Length == 4 & linesDictionary[key].Equals("else"))
                {
                    ifElseDictionary[key] = "else";
                }
            }

            Stack<int> stack = new Stack<int>();

            foreach (var (key, value) in ifElseDictionary)
            {
                if (value.Equals("if"))
                {
                    stack.Push(key);
                }
                else if (value.Equals("else"))
                {
                    if (stack.Count > 0)
                    {
                        int index = stack.Pop();
                        result[index] = key;
                    }
                }
            }

            return result;
        }
    }
}